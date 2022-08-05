using System;
using System.Linq;

namespace SqlKata
{
    public partial class Query
    {

        public Query Combine(string operation, bool all, Query query)
        {
            if (this.Method != "select" || query.Method != "select")
            {
                throw new InvalidOperationException("Only select queries can be combined.");
            }

            return AddComponent("combine", new Combine
            {
                Query = query,
                Operation = operation,
                All = all,
            });
        }

        public Query CombineRaw(string sql, params object[] bindings)
        {
            if (this.Method != "select")
            {
                throw new InvalidOperationException("Only select queries can be combined.");
            }

            return AddComponent("combine", new RawCombine
            {
                Expression = sql,
                Bindings = bindings,
            });
        }

        public Query Union(Query query, bool all = false)
        {
            return Combine("union", all, query);
        }

        public Query UnionAll(Query query)
        {
            return Union(query, true);
        }

        public Query Union(Action<Query> callback, bool all = false)
        {
            var query = new Query();
            callback(query);
            return Union(query, all);
        }

        public Query UnionAll(Action<Query> callback)
        {
            return Union(callback, true);
        }

        public Query UnionRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

        public Query Except(Query query, bool all = false)
        {
            return Combine("except", all, query);
        }

        public Query ExceptAll(Query query)
        {
            return Except(query, true);
        }

        public Query Except(Action<Query> callback, bool all = false)
        {
            var query = new Query();
            callback(query);
            return Except(query, all);
        }

        public Query ExceptAll(Action<Query> callback)
        {
            return Except(callback, true);
        }
        public Query ExceptRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

        public Query Intersect(Query query, bool all = false)
        {
            return Combine("intersect", all, query);
        }

        public Query IntersectAll(Query query)
        {
            return Intersect(query, true);
        }

        public Query Intersect(Action<Query> callback, bool all = false)
        {
            var query = new Query();
            callback(query);
            return Intersect(query, all);
        }

        public Query IntersectAll(Action<Query> callback)
        {
            return Intersect(callback, true);
        }
        public Query IntersectRaw(string sql, params object[] bindings) => CombineRaw(sql, bindings);

    }
}
