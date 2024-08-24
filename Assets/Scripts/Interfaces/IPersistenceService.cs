using System.Collections.Generic;

namespace Calculator.Interfaces
{
    public interface IPersistenceService
    {
        public void SaveLastInput(string input);
        public string LoadLastInput();
        
        public void SaveHistory(IEnumerable<string> history);
        public ICollection<string> LoadHistory();
    }
}