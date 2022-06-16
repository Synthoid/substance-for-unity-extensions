using UnityEngine;
using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;
using System.Text;

namespace SOS.SubstanceExtensionsEditor
{
    public class LabelSearchTrie
    {
        public string content = "";
        public List<LabelSearchTrie> branches = new List<LabelSearchTrie>();

        private string path = "";
        public int level = 0;
        public int index = 0;

        private static Texture2D emptyTexture = null;

        private static Texture2D EmptyTexture
        {
            get
            {
                if(emptyTexture == null)
                {
                    emptyTexture = new Texture2D(2, 2);

                    emptyTexture.SetPixels32(new Color32[4] {
                        Color.clear,
                        Color.clear,
                        Color.clear,
                        Color.clear
                    });
                    emptyTexture.Apply();
                }

                return emptyTexture;
            }
        }


        public LabelSearchTrie(string content, string prefix)
        {
            this.content = content;
            this.path = prefix;
            branches = new List<LabelSearchTrie>();
        }

        //TODO: Add support for preserving image content in labels?
        public LabelSearchTrie(string content, IList<GUIContent> labels) : this(content, "")
        {
            for(int i=0; i < labels.Count; i++)
            {
                int index = i;
                int startIndex = 0;
                string[] path = labels[i].text.Split('/');
                StringBuilder currentPath = new StringBuilder(path[startIndex]);
                LabelSearchTrie data = GetBranch(path[startIndex], "");

                startIndex++;

                while(startIndex < path.Length)
                {
                    data.level = startIndex;

                    data = data.GetBranch(path[startIndex], currentPath.ToString());

                    currentPath.Append('/');
                    currentPath.Append(path[startIndex]);

                    startIndex++;
                }

                data.index = index;
                data.level = path.Length;
            }
        }


        public LabelSearchTrie(string content, IList<string> labels) : this(content, "")
        {
            for(int i = 0; i < labels.Count; i++)
            {
                int index = i;
                int startIndex = 0;
                string[] path = labels[i].Split('/');
                StringBuilder currentPath = new StringBuilder(path[startIndex]);
                LabelSearchTrie data = GetBranch(path[startIndex], "");

                startIndex++;

                while(startIndex < path.Length)
                {
                    data.level = startIndex;

                    data = data.GetBranch(path[startIndex], currentPath.ToString());

                    currentPath.Append('/');
                    currentPath.Append(path[startIndex]);

                    startIndex++;
                }

                data.index = index;
                data.level = path.Length;
            }
        }


        public void Clear()
        {
            content = "";
            branches.Clear();

            path = "";
            level = 0;
            index = 0;
        }


        public LabelSearchTrie GetBranch(string content, string prefix)
        {
            if(this.content == content) return this;

            //Path doesn't start with the current path prefix, so ignore it.
            if(!string.IsNullOrEmpty(path) && !prefix.StartsWith(path)) return null;

            for(int i=0; i < branches.Count; i++)
            {
                LabelSearchTrie data = branches[i].GetBranch(content, prefix);

                if(data != null) return data;
            }

            return AddBranch(content);
        }


        public LabelSearchTrie AddBranch(string content)
        {
            LabelSearchTrie newBranch = new LabelSearchTrie(content, $"{(string.IsNullOrEmpty(path) ? "" : $"{path}/")}{content}");

            branches.Add(newBranch);

            return newBranch;
        }


        public List<SearchTreeEntry> GetSearchTree()
        {
            List<SearchTreeEntry> searchTree = new List<SearchTreeEntry>();

            if(branches.Count == 0)
            {
                searchTree.Add(new SearchTreeEntry(new GUIContent(content, EmptyTexture))
                {
                    level = this.level,
                    userData = this.index
                });
            }
            else
            {
                searchTree.Add(new SearchTreeGroupEntry(new GUIContent(content), level));

                for(int i=0; i < branches.Count; i++)
                {
                    searchTree.AddRange(branches[i].GetSearchTree());
                }
            }

            return searchTree;
        }
    }
}