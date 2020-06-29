using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Microsoft.EntityFrameworkCore;
/*
 * 
place orders to store locations for customers => Insert
add a new customer => Insert
search customers by name => Find
display details of an order => Find
display all order history of a store location => List
display all order history of a customer => List
input validation
exception handling
*/
namespace App.DataAccess
{
    public interface IList<T> 
    {
        IEnumerable<T> List();
        IEnumerable<T> List(Expression<Func<T, bool>> predicate);
    }
    public interface IFind<T>
    {
        T Find(int id);
        T Find(string name, Func<T, bool> L);
    }
    public interface IInsert<T>
    {
        void Add(T entity);
    }
    public abstract class EntityBase
    {
        public int Id { get; protected set; }
    }
}
