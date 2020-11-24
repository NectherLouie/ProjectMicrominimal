using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Micro
{
    public class WaypointEditorWindow : EditorWindow
    {
        private bool closeLoop = false;

        [MenuItem("Tools/Waypoint Editor")]
        public static void Open()
        {
            GetWindow<WaypointEditorWindow>();
        }

        public Transform root;

        private void OnGUI()
        {
            SerializedObject obj = new SerializedObject(this);

            EditorGUILayout.PropertyField(obj.FindProperty("root"));

            if (root == null)
            {
                EditorGUILayout.HelpBox("Root transform must be selected. Please assign a root transform", MessageType.Warning);
            }
            else
            {
                EditorGUILayout.BeginVertical("box");
                DrawButtons();
                EditorGUILayout.EndVertical();
            }

            obj.ApplyModifiedProperties();
        }

        void DrawButtons()
        {
            if (GUILayout.Button("Create Waypoint"))
            {
                CreateWaypoint();
            }

            if (Selection.activeGameObject != null && Selection.activeGameObject.GetComponent<WaypointNode>())
            {
                if (GUILayout.Button("Remove Waypoint"))
                {
                    RemoveWaypoint();
                }
            }

            closeLoop = GUILayout.Toggle(closeLoop, "Close Loop");
            CloseLoop();
        }

        void CreateWaypoint()
        {
            string name = "Waypoint(" + root.childCount + ")";
            GameObject waypointObject = new GameObject(name, typeof(WaypointNode));
            waypointObject.transform.SetParent(root, false);

            waypointObject.AddComponent<SphereCollider>();
            waypointObject.GetComponent<SphereCollider>().isTrigger = true;

            WaypointNode node = waypointObject.GetComponent<WaypointNode>();
            if (root.childCount > 1)
            {
                node.prev = root.GetChild(root.childCount - 2).GetComponent<WaypointNode>();
                node.prev.next = node;

                node.transform.position = node.prev.transform.position;

                CloseLoop();
            }

            Selection.activeGameObject = node.gameObject;
        }

        private void CloseLoop()
        {
            if (root.childCount > 1)
            {
                WaypointNode first = root.GetChild(0).GetComponent<WaypointNode>();
                WaypointNode last = root.GetChild(root.childCount - 1).GetComponent<WaypointNode>();

                if (closeLoop)
                {
                    first.prev = last;
                    last.next = first;
                }
                else
                {
                    first.prev = null;
                    last.next = null;
                }
            }
        }

        void RemoveWaypoint()
        {
            WaypointNode selectedNode = Selection.activeGameObject.GetComponent<WaypointNode>();

            if (selectedNode.next != null)
            {
                selectedNode.next.prev = selectedNode.prev;
            }

            if (selectedNode.prev != null)
            {
                selectedNode.prev.next = selectedNode.next;

                Selection.activeGameObject = selectedNode.prev.gameObject;
            }

            DestroyImmediate(selectedNode.gameObject);
        }
    }
}
