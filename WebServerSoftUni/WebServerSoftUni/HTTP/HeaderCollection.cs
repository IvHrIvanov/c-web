using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebServerSoftUni.HTTP
{
    public class HeaderCollection : IEnumerable<Header>
    {
        private readonly Dictionary<string, Header> headers = new Dictionary<string, Header>();

        public int Count => headers.Count;

        public void Add(string name, string value) => this.headers[name] = new Header(name, value);
        

        public IEnumerator<Header> GetEnumerator() => this.headers.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public string this[string name]=>this.headers[name].Value;
        public bool Contains(string name) => headers.ContainsKey(name);

    }
}
