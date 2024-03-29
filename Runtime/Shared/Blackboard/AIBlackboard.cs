using System.Collections.Generic;
using UnityEngine;

namespace BT.AI.Blackboard
{
    [System.Serializable]
    public class AIBlackboard
    {
        private Dictionary<string, Entry> entries;

        public AIBlackboard()
        {
            entries = new Dictionary<string, Entry>();
        }

        /// <summary>
        /// Add (if value is not in blackboard) or update value (if value is in blackboard)
        /// </summary>
        /// <returns></returns>
        public bool TryAddOrUpdateValue<T>(string name, T value)
        {
            if (entries.ContainsKey(name))
            {
                return TryUpdateValue(name, value);
            }
            else
            {
                AddEntry<T>(name, value);
                return true;
            }
        }

        /// <summary>
        /// Try and get the value of a given entry from the blackboard
        /// </summary>
        /// <returns></returns>
        public bool TryGetValue<T>(string name, out T value)
        {
            value = default(T);
            if (!entries.ContainsKey(name))
            {
                return false;
            }

            Entry entry = entries[name];

            if (!entry.IsValueOfType<T>())
            {
                return false;
            }

            value = entry.GetValueAs<T>();
            return true;
        }

        /// <summary>
        /// Check if the blackboard contains a particular value
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool ContainsValue(string name)
        {
            return entries.ContainsKey(name);
        }

        /// <summary>
        /// Try removing a value from the blackboard
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public bool TryRemoveValue(string name)
        {
            if (!entries.ContainsKey(name))
            {
                return false;
            }

            entries.Remove(name);
            return true;
        }

        /// <summary>
        /// Try and update the value of an exisiting entry
        /// </summary>
        /// <returns>If value was updated</returns>
        private bool TryUpdateValue<T>(string name, T value)
        {
            if (!entries.ContainsKey(name))
            {
                return false;
            }

            Entry entry = entries[name];

            if (!entry.IsValueOfType<T>())
            {
                return false;
            }
            
            entry.UpdateValue(value);
            return true;
        }

        /// <summary>
        /// Add an entry to the blackboard
        /// </summary>
        /// <returns></returns>
        private Entry AddEntry<T>(string name, T value)
        {
            Entry newEntry = new Entry(name, value);
            entries.Add(name, newEntry);
            return newEntry;
        }
    }
}
