using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace SOS.SubstanceExtensionsEditor
{
    /// <summary>
    /// Search provider that displays an array of labels and returns the selected label's index.
    /// </summary>
    public class LabelSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        /// <summary>
        /// Labels shown in the search window.
        /// </summary>
        public IList<GUIContent> Labels { get; private set; } = null;

        private System.Action<int> SelectionCallback { get; set; } = null;
        private GUIContent Title { get; set; } = null;

        public LabelSearchProvider Initialize(IList<GUIContent> labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            Initialize(selectionCallback, title);

            this.Labels = labels;

            return this;
        }


        public LabelSearchProvider Initialize(IList<string> labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            Initialize(selectionCallback, title);

            this.Labels = new GUIContent[labels.Count];

            for(int i=0; i < labels.Count; i ++)
            {
                Labels[i] = new GUIContent(labels[i]);
            }

            return this;
        }


        private void Initialize(System.Action<int> selectionCallback, GUIContent title)
        {
            this.SelectionCallback = selectionCallback;

            if(title == null) title = new GUIContent("List");
            if(string.IsNullOrEmpty(title.text)) title.text = "List";

            this.Title = title;
        }


        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTree = new List<SearchTreeEntry>();
            List<string> groups = new List<string>();

            if(!string.IsNullOrEmpty(Title.text)) searchTree.Add(new SearchTreeGroupEntry(new GUIContent(Title)));

            for(int i=0; i < Labels.Count; i++)
            {
                int index = i;
                string[] entryTitles = Labels[i].text.Split('/');
                string groupName = "";

                for(int j=0; j < entryTitles.Length - 1; j++)
                {
                    groupName += entryTitles[j];

                    if(!groups.Contains(groupName))
                    {
                        searchTree.Add(new SearchTreeGroupEntry(new GUIContent(entryTitles[j]), j + 1));
                        groups.Add(groupName);
                    }

                    groupName += "/";
                }

                SearchTreeEntry entry = new SearchTreeEntry(new GUIContent(entryTitles[entryTitles.Length - 1]))
                {
                    level = entryTitles.Length,
                    userData = index
                };

                searchTree.Add(entry);
            }

            return searchTree;
        }


        public bool OnSelectEntry(SearchTreeEntry SearchTreeEntry, SearchWindowContext context)
        {
            if(SelectionCallback != null) SelectionCallback.Invoke((int)SearchTreeEntry.userData);

            return true;
        }
    }
}