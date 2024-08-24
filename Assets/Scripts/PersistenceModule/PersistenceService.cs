using System;
using System.Collections.Generic;
using Calculator.Interfaces;
using UnityEngine;

namespace Calculator.PersistenceModule
{
    public class PersistenceService : IPersistenceService
    {
        private const string lastInputKey = "LastInput";
        private const string historyKey = "History";
        
        public void SaveLastInput(string input)
        {
            PlayerPrefs.SetString(lastInputKey, input);
            PlayerPrefs.Save();
        }

        public string LoadLastInput()
        {
            return PlayerPrefs.GetString(lastInputKey, string.Empty);
        }

        public void SaveHistory(IEnumerable<string> history)
        {
            PlayerPrefs.SetString(historyKey, string.Join(",", history));
            PlayerPrefs.Save();
        }

        public ICollection<string> LoadHistory()
        {
            string historyString = PlayerPrefs.GetString(historyKey, string.Empty);
            return historyString.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

        }
    }
}