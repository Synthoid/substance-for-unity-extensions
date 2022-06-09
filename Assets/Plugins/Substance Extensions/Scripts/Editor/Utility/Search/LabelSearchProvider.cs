using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace SOS.SubstanceExtensionsEditor
{
    public class LabelSearchProvider : ScriptableObject, ISearchWindowProvider
    {
        public GUIContent[] Labels { get; private set; } = null;

        private System.Action<int> SelectionCallback { get; set; } = null;
        private GUIContent Title { get; set; } = null;

        public LabelSearchProvider Initialize(GUIContent[] labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            this.Labels = labels;
            this.SelectionCallback = selectionCallback;
            this.Title = title;

            return this;
        }


        public LabelSearchProvider Initialize(string[] labels, System.Action<int> selectionCallback, GUIContent title=default)
        {
            this.Labels = new GUIContent[labels.Length];
            this.SelectionCallback = selectionCallback;
            this.Title = title;

            for(int i=0; i < labels.Length; i ++)
            {
                Labels[i] = new GUIContent(labels[i]);
            }

            return this;
        }


        public List<SearchTreeEntry> CreateSearchTree(SearchWindowContext context)
        {
            List<SearchTreeEntry> searchTree = new List<SearchTreeEntry>();
            List<string> groups = new List<string>();

            if(!string.IsNullOrEmpty(Title.text)) searchTree.Add(new SearchTreeGroupEntry(new GUIContent(Title)));

            for(int i=0; i < Labels.Length; i++)
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
