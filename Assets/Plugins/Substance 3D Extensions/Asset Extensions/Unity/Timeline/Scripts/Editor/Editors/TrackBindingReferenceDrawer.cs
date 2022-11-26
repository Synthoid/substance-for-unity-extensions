using UnityEngine;
using UnityEngine.Timeline;
using UnityEditor;
using UnityEditor.Timeline;

namespace SOS.SubstanceExtensionsEditor.Timeline
{
    /// <summary>
    /// Base class for drawers of fields that reference a Timeline track's bound asset.
    /// </summary>
    public abstract class TrackBindingReferenceDrawer : PropertyDrawer
    {
        public int GetSelectedClipCount()
        {
            return TimelineEditor.selectedClips.Length;
        }


        public Object GetBinding()
        {
            TimelineClip[] selectedClips = TimelineEditor.selectedClips;

            if (selectedClips.Length == 0) return null;

            return TimelineEditor.inspectedDirector.GetGenericBinding(selectedClips[0].GetParentTrack());
        }
    }


    public abstract class TrackBindingReferenceDrawer<T> : TrackBindingReferenceDrawer where T : UnityEngine.Object
    {


        public T GetBindingCast()
        {
            return (T)GetBinding();
        }
    }
}