using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Kinect = Windows.Kinect;
using UnityEngine.UI;

public class BodySourceView : MonoBehaviour 
{
    public Material BoneMaterial;
    public GameObject BodySourceManager;
    public GameObject JointPrefab;
    public GameObject ColliderPrefab;

    private Dictionary<ulong, GameObject> _Bodies = new Dictionary<ulong, GameObject>();
    private Dictionary<int, ulong> _PlayersId = new Dictionary<int, ulong>();
    private BodySourceManager _BodyManager;

    [SerializeField]
    private SpriteRenderer[] markers;
    [SerializeField]
    private Text playersCountUI;


    private Dictionary<Kinect.JointType, Kinect.JointType> _BoneMap = new Dictionary<Kinect.JointType, Kinect.JointType>()
    {
        { Kinect.JointType.FootLeft, Kinect.JointType.AnkleLeft },
        { Kinect.JointType.AnkleLeft, Kinect.JointType.KneeLeft },
        { Kinect.JointType.KneeLeft, Kinect.JointType.HipLeft },
        { Kinect.JointType.HipLeft, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.FootRight, Kinect.JointType.AnkleRight },
        { Kinect.JointType.AnkleRight, Kinect.JointType.KneeRight },
        { Kinect.JointType.KneeRight, Kinect.JointType.HipRight },
        { Kinect.JointType.HipRight, Kinect.JointType.SpineBase },
        
        { Kinect.JointType.HandTipLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.ThumbLeft, Kinect.JointType.HandLeft },
        { Kinect.JointType.HandLeft, Kinect.JointType.WristLeft },
        { Kinect.JointType.WristLeft, Kinect.JointType.ElbowLeft },
        { Kinect.JointType.ElbowLeft, Kinect.JointType.ShoulderLeft },
        { Kinect.JointType.ShoulderLeft, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.HandTipRight, Kinect.JointType.HandRight },
        { Kinect.JointType.ThumbRight, Kinect.JointType.HandRight },
        { Kinect.JointType.HandRight, Kinect.JointType.WristRight },
        { Kinect.JointType.WristRight, Kinect.JointType.ElbowRight },
        { Kinect.JointType.ElbowRight, Kinect.JointType.ShoulderRight },
        { Kinect.JointType.ShoulderRight, Kinect.JointType.SpineShoulder },
        
        { Kinect.JointType.SpineBase, Kinect.JointType.SpineMid },
        { Kinect.JointType.SpineMid, Kinect.JointType.SpineShoulder },
        { Kinect.JointType.SpineShoulder, Kinect.JointType.Neck },
        { Kinect.JointType.Neck, Kinect.JointType.Head },
    };
    
    void Update () 
    {
        #region Get Kinect Data
        if (BodySourceManager == null)
        {
            return;
        }
        
        _BodyManager = BodySourceManager.GetComponent<BodySourceManager>();
        if (_BodyManager == null)
        {
            return;
        }
        
        Kinect.Body[] data = _BodyManager.GetData();
        if (data == null)
        {
            return;
        }

        List<ulong> trackedIds = new List<ulong>();
        foreach(var body in data)
        {
            if (body == null)
            {
                continue;
            }
                
            if(body.IsTracked)
            {
                trackedIds.Add (body.TrackingId);
            }
        }
        playersCountUI.text = (trackedIds.Count) + " Joueurs";

        #endregion

        #region Delete Kinect Bodies
        List<ulong> knownIds = new List<ulong>(_Bodies.Keys);
        
        // First delete untracked bodies
        foreach(ulong trackingId in knownIds)
        {
            if(!trackedIds.Contains(trackingId))
            {
                //Destroy body object
                Destroy(_Bodies[trackingId]);
                //Remove from list
                _Bodies.Remove(trackingId);
                if (_PlayersId.ContainsValue(trackingId))
                {
                    for (int i = 0; i < _PlayersId.Count; i++)
                    {
                        if (_PlayersId.ContainsKey(i) && _PlayersId[i] == trackingId)
                        {
                            //markers[i].color = new Color(1f, 1f, 1f, 0f);
                            _PlayersId.Remove(i);
                        }
                    }
                }
            }
        }
        #endregion

        #region Create Kinect Bodies
        foreach (var body in data)
        {
            //If no body, skip
            if (body == null)
            {
                continue;
            }
            
            if(body.IsTracked)
            {
                //if body isn't tracked, create body
                if(!_Bodies.ContainsKey(body.TrackingId))
                {
                    _Bodies[body.TrackingId] = CreateBodyObject(body.TrackingId);
                }
                //Update positions
                RefreshBodyObject(body, _Bodies[body.TrackingId]);
            }
        }



        #endregion
    }

    private GameObject CreateBodyObject(ulong id)
    {
        //Create body parent
        GameObject body = new GameObject("Body:" + id);
        GameObject collider = Instantiate<GameObject>(ColliderPrefab);
        collider.name = ""+id;
        collider.transform.parent = body.transform;
        collider.transform.position = Vector3.zero;
        //Create joints
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            //Create object
            GameObject jointObj = Instantiate<GameObject>(JointPrefab);


            LineRenderer lr = jointObj.AddComponent<LineRenderer>();
            lr.SetVertexCount(2);
            lr.material = BoneMaterial;
            lr.SetWidth(0.05f, 0.05f);
            
            jointObj.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            jointObj.name = jt.ToString();

            //Parent to body
            jointObj.transform.parent = body.transform;
        }
        
        return body;
    }
    
    private void RefreshBodyObject(Kinect.Body body, GameObject bodyObject)
    {
        //Update current joints from body
        for (Kinect.JointType jt = Kinect.JointType.SpineBase; jt <= Kinect.JointType.ThumbRight; jt++)
        {
            //Get new target position
            Kinect.Joint sourceJoint = body.Joints[jt];
            Kinect.Joint? targetJoint = null;

            if (_BoneMap.ContainsKey(jt))
            {
                targetJoint = body.Joints[_BoneMap[jt]];
            }

            // Get joint, set new position
            Transform jointObj = bodyObject.transform.Find(jt.ToString());
            jointObj.localPosition = GetVector3FromJoint(sourceJoint);

            LineRenderer lr = jointObj.GetComponent<LineRenderer>();
            if (targetJoint.HasValue)
            {
                lr.SetPosition(0, jointObj.localPosition);
                lr.SetPosition(1, GetVector3FromJoint(targetJoint.Value));
                lr.SetColors(GetColorForState(sourceJoint.TrackingState), GetColorForState(targetJoint.Value.TrackingState));
            }
            else
            {
                lr.enabled = false;
            }
            if (GetPlayerFromId(body.TrackingId) != -1)
            {
                bodyObject.transform.GetChild(0).transform.position = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]);
               /* if (markers.Length > 0)
                {
                    markers[GetPlayerFromId(body.TrackingId)].transform.position = GetVector3FromJoint(body.Joints[Kinect.JointType.Head]);
                    markers[GetPlayerFromId(body.TrackingId)].color = new Color(1f, 1f, 1f, 1f);
                }*/
            }
        }
    }
    
    private static Color GetColorForState(Kinect.TrackingState state)
    {
        switch (state)
        {
        case Kinect.TrackingState.Tracked:
            return Color.green;

        case Kinect.TrackingState.Inferred:
            return Color.red;

        default:
            return Color.black;
        }
    }
    
    private static Vector3 GetVector3FromJoint(Kinect.Joint joint)
    {
        return new Vector3(joint.Position.X * 10, joint.Position.Y * 10, joint.Position.Z * 10);
    }

    private int FirstAvailableId()
    {
        for(int i = 0; i < 4; i++)
        {
            if (!_PlayersId.ContainsKey(i))
            {
                return i;
            }
        }
        return -1;
    }

    private int GetPlayerFromId(ulong trackingId)
    {
        if (!_PlayersId.ContainsValue(trackingId))
        {
            int id = FirstAvailableId();
            if (id != -1)
            {
                _PlayersId.Add(id, trackingId);
                return id;
            }
        } else
        {
            for (int i = 0; i < _PlayersId.Count; i++)
            {
                if (_PlayersId.ContainsKey(i) && _PlayersId[i] == trackingId)
                {
                    return i;
                }
            }
        }
        return -1;
    }

    public int GetTrackedBodiesCount()
    {
        return _Bodies.Count;
    }

    public void HideUnusedImage()
    {
        for (int i = 0; i < markers.Length; i++)
        {
            Color hide = new Color(0, 0, 0, 0);
            markers[i].color = hide;
        }
    }

    public void AssignBody(int id, ulong tracker)
    {
        if (_PlayersId.ContainsKey(id))
        {
            //markers[id].color = new Color(1f, 1f, 1f, 0f);
            _PlayersId.Remove(id);
            _PlayersId.Add(id, tracker);
        } else
        {
            _PlayersId.Add(id, tracker);
        }
    }
}
