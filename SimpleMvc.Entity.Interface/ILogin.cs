using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleMvc.Entity.Interface
{
    public interface ILogin<TKey> where TKey:class
    {
        TKey Id { get; set; }
    }
}
