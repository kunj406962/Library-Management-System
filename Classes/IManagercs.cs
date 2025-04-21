using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CPRG211FinalProject.Classes
{
    public interface IManagercs<T>
    {
        static void Add() { }
        static void Update(T data) { }
        static void Delete(T data) { }
    }
}
