using System;

namespace SqlKata
{
    public partial class Query
    {

        private Query Join(Action<Join> callback)
        {
            var join = new Join().AsInner();
            callback(join);

            return AddComponent("join", new BaseJoin
            {
                Join = join
            });
        }

        public Query Join(
            string table,
            string first,
            string second,
            string op = "=",
            string type = "inner join"
        )
        {
            return Join(j => j.JoinWith(table).WhereColumns(first, op, second).AsType(type));
        }

        public Query Join(string table, Action<Join> callback, string type = "inner join")
        {
            return Join(j => j.JoinWith(table).Where(callback).AsType(type));
        }

        public Query Join(Query query, Action<Join> onCallback, string type = "inner join")
        {
            return Join(j => j.JoinWith(query).Where(onCallback).AsType(type));
        }

        public Query LeftJoin(string table, string first, string second, string op = "=")
        {
            return Join(table, first, second, op, "left join");
        }

        public Query LeftJoin(string table, Action<Join> callback)
        {
            return Join(table, callback, "left join");
        }

        public Query LeftJoin(Query query, Action<Join> onCallback)
        {
            return Join(query, onCallback, "left join");
        }

        public Query RightJoin(string table, string first, string second, string op = "=")
        {
            return Join(table, first, second, op, "right join");
        }

        public Query RightJoin(string table, Action<Join> callback)
        {
            return Join(table, callback, "right join");
        }

        public Query RightJoin(Query query, Action<Join> onCallback)
        {
            return Join(query, onCallback, "right join");
        }

        public Query CrossJoin(string table)
        {
            return Join(j => j.JoinWith(table).AsCross());
        }

    }
}
