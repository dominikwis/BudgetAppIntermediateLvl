using BudgetAppIntermediate.Entity;
using System.Text.Json;

namespace BudgetAppIntermediate.Repositories
{
    public class FileRepository<T> : IRepository<T>, IEventRepository<T>
        where T : BillBase, new()
    {
        private List<T> _items;
        public static readonly List<string> _actions = new List<string>
        {
            "BillAdded",
            "BillDeleted",
            "AllBillsDeleted"
        };

        private readonly string _fileName = "bills.txt";

        public event EventHandler<BillEventArgs<T>> ItemAdded;
        public event EventHandler<BillEventArgs<T>> ItemRemoved;
        public event EventHandler<BillEventArgs<T>> AllItemRemoved;

        public FileRepository()
        {
            _items = new List<T>();

            this.LoadDataFromFile();
        }

        private void LoadDataFromFile()
        {
            if (File.Exists(_fileName))
            {
                string json = File.ReadAllText(_fileName);

                if(!string.IsNullOrWhiteSpace(json))
                {
                    _items = JsonSerializer.Deserialize<List<T>>(json);
                }
            }

            for (int id = 0; id < _items.Count; id++)
            {
                _items[id].Id = id + 1;
            }
        }

        public IEnumerable<T> GetAll()
        {
            if (_items.Count > 0)
            {
                this.LoadDataFromFile();
                return _items.ToList();
            }
            else
            {
                throw new Exception("Failure - the list of the bills is empty");
            }
        }

        public T GetById(int Id)
        {
            return _items.Single(item => item.Id == Id);
        }

        public void Add(T item)
        {
            item.Id = _items.Count + 1;
            _items.Add(item);
            this.ItemAdded?.Invoke(this, new BillEventArgs<T>("BillAdded", item));
        }

        public void RemoveById(int id)
        {
            var itemToRemove = _items.FirstOrDefault(item => item.Id == id);

            if (itemToRemove != null)
            {
                _items.Remove(itemToRemove);
                this.ItemRemoved?.Invoke(this, new BillEventArgs<T>("BillRemoved", itemToRemove));
                this.Save();
            }
        }

        public void RemoveAll()
        {
            if (_items.Count > 0)
            {
                _items.Clear();
                this.AllItemRemoved?.Invoke(this, new BillEventArgs<T>("AllBillsDeleted", null));
                this.Save();
            }
            else
            {
                throw new Exception("Failure - the list of the bills is already empty");
            }
        }

        public void Save()
        {
            var json = JsonSerializer.Serialize(_items);

            File.WriteAllText(_fileName, json);
        }
    }
}
