﻿// GraphView
// 
// Copyright (c) 2015 Microsoft Corporation
// 
// All rights reserved. 
// 
// MIT License
// 
// Permission is hereby granted, free of charge, to any person obtaining a copy of
// this software and associated documentation files (the "Software"), to deal in
// the Software without restriction, including without limitation the rights to
// use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
// the Software, and to permit persons to whom the Software is furnished to do so,
// subject to the following conditions:
// 
// The above copyright notice and this permission notice shall be included in all
// copies or substantial portions of the Software.
// 
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
// FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
// COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
// IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
// CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
// 

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Microsoft.SqlServer.TransactSql.ScriptDom;

namespace GraphView
{
 
    public abstract partial class WTableReference : WSqlFragment
    {
        /// <summary>
        /// Returns a list of table references in this WTableReference expression.
        /// </summary>
        /// <returns>A list of table references</returns>
        internal virtual IList<string> TableAliases()
        {
            return new List<string>(); 
        }

    }
    public abstract partial class WTableReferenceWithAlias : WTableReference 
    {
        internal Identifier Alias { set; get; }
    }

    public abstract partial class WTableReferenceWithAliasAndColumns : WTableReferenceWithAlias
    {
        internal IList<Identifier> Columns { set; get; }
    }

    public partial class WSpecialNamedTableReference : WTableReferenceWithAlias
    {
        internal WSchemaObjectName TableObjectName { get; set; }
        internal IList<WTableHint> TableHints { set; get; }

        internal override bool OneLine()
        {
            return true;
        }

        internal override string ToString(string indent)
        {
            string tableObjectString = null;
            if (TableObjectName.ServerIdentifier != null)
            {
                tableObjectString = string.Format("[{0}]", TableObjectName.ServerIdentifier.Value);
            }
            if (TableObjectName.DatabaseIdentifier != null)
            {
                tableObjectString = string.Format(tableObjectString == null ? "[{0}]" : ".[{0}]", TableObjectName.DatabaseIdentifier.Value);
            }
            if (TableObjectName.SchemaIdentifier != null)
            {
                tableObjectString = string.Format(tableObjectString == null ? "[{0}]" : ".[{0}]", TableObjectName.SchemaIdentifier.Value);
            }
            if (TableObjectName.BaseIdentifier != null)
            {
                tableObjectString = string.Format(tableObjectString == null ? "{0}" : ".{0}", TableObjectName.BaseIdentifier.Value);
            }

            var sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", indent, tableObjectString);
            if (Alias != null)
                sb.Append(" AS " + string.Format("[{0}]", Alias.Value));
            if (TableHints != null && TableHints.Count > 0)
            {
                sb.Append(" WITH (");
                var index = 0;
                for (var count = TableHints.Count; index < count; ++index)
                {
                    if (index > 0)
                        sb.Append(", ");
                    sb.Append(TableHints[index]);
                }
                sb.Append(')');
            }

            return sb.ToString();
        }
    }

    public partial class WNamedTableReference : WTableReferenceWithAlias
    {
        internal WSchemaObjectName TableObjectName { set; get; }

        internal IList<WTableHint> TableHints { set; get; }

        public Identifier ExposedName
        {
            get
            {
                if (Alias != null)
                    return Alias;
                if (TableObjectName != null)
                    return TableObjectName.BaseIdentifier;
                return null;
            }
        }

        // SchemaObjectName cannot be modified externally. 
        // We use this field to add a new table reference to the parsed tree.
        public string TableObjectString { set; get; }

        internal override bool OneLine()
        {
            return true;
        }

        internal override string ToString(string indent)
        {
            TableObjectString = TableObjectName == null ? TableObjectString : TableObjectName.ToString();

            var sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", indent, TableObjectString);
            if (Alias != null)
                sb.Append(" AS " + string.Format("[{0}]", Alias.Value));
            if (TableHints != null && TableHints.Count > 0)
            {
                sb.Append(" WITH (");
                var index = 0;
                for (var count = TableHints.Count; index < count; ++index)
                {
                    if (index > 0)
                        sb.Append(", ");
                    sb.Append(TableHints[index]);
                }
                sb.Append(')');
            }

            return sb.ToString();
        }

        internal override IList<string> TableAliases()
        {
            var aliases = new List<string>(1) { Alias != null ? Alias.Value : TableObjectName.BaseIdentifier.Value };

            return aliases;
        }

        internal static Tuple<string, string> SchemaNameToTuple(WSchemaObjectName name)
        {
            return
                name == null
                    ? null
                    : new Tuple<string, string>(
                        name.SchemaIdentifier == null
                            ? "dbo"
                            : name.SchemaIdentifier.Value.ToLower(CultureInfo.CurrentCulture),
                        name.BaseIdentifier.Value.ToLower(CultureInfo.CurrentCulture));
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }
    }

    public partial class WQueryDerivedTable : WTableReferenceWithAliasAndColumns
    {
        internal WSelectQueryExpression QueryExpr { set; get; }

        internal override bool OneLine()
        {
            return false;
        }

        internal override string ToString(string indent)
        {
            var sb = new StringBuilder(32);

            sb.AppendFormat("{0}(\r\n", indent);
            sb.AppendFormat("{0}{1}\r\n", indent, QueryExpr.ToString(indent));
            sb.AppendFormat("{0}) AS [{1}]", indent, Alias.Value);

            if (Columns != null && Columns.Count > 0)
            {
                sb.Append('(');
                for (var i = 0; i < Columns.Count; i++)
                {
                    if (i > 0)
                    {
                        sb.Append(", ");
                    }
                    sb.Append(Columns[i].Value);
                }
                sb.Append(')');
            }

            return sb.ToString();
        }

        internal override IList<string> TableAliases()
        {
            var aliases = new List<string>(1) { Alias.Value };

            return aliases;
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (QueryExpr != null)
                QueryExpr.Accept(visitor);
            base.AcceptChildren(visitor);
        }
    }

    public partial class WSchemaObjectFunctionTableReference : WTableReferenceWithAliasAndColumns
    {
        public IList<WScalarExpression> Parameters { get; set; }

        public WSchemaObjectName SchemaObject { get; set; }

        internal override bool OneLine()
        {
            return true;
        }

        internal override string ToString(string indent)
        {
            var sb = new StringBuilder();
            sb.AppendFormat("{0}{1}", indent, SchemaObject);
            if (Parameters != null)
            {
                var index = 0;
                sb.Append("(");
                for (var count = Parameters.Count; index < count; ++index)
                {
                    if (index > 0)
                        sb.Append(", ");
                    sb.Append(Parameters[index]);
                }
                sb.Append(")");
            }
            if (Alias != null)
                sb.Append(" AS [" + Alias.Value + "]");

            return sb.ToString();
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (SchemaObject != null)
                SchemaObject.Accept(visitor);
            if (Parameters != null)
            {
                var index = 0;
                for (var count = Parameters.Count; index < count; ++index)
                {
                    Parameters[index].Accept(visitor);
                }
            }
            base.AcceptChildren(visitor);
        }
    }

    public partial class WJoinParenthesisTableReference : WTableReference
    {
        internal WTableReference Join { get; set; }

        internal override bool OneLine()
        {
            return Join.OneLine();
        }

        internal override string ToString(string indent)
        {
            if (OneLine())
            {
                return string.Format("{0}({1})", indent, Join.ToString());
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("{0}(\r\n", indent);
                sb.AppendFormat("{0}{1}\r\n", indent, Join.ToString());
                sb.AppendFormat("{0})\r\n", indent);

                return sb.ToString();
            }
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (Join != null)
            {
                Join.Accept(visitor);
            }
            base.AcceptChildren(visitor);
        }
    }

    public abstract partial class WJoinTableReference : WTableReference
    {
        internal WTableReference FirstTableRef { set; get; }
        internal WTableReference SecondTableRef { set; get; }

        internal override IList<string> TableAliases()
        {
            var a1 = FirstTableRef.TableAliases();
            var a2 = SecondTableRef.TableAliases();

            var aliases = new List<string>(a1.Count + a2.Count);
            aliases.AddRange(a1);
            aliases.AddRange(a2);

            return aliases;
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (FirstTableRef != null)
                FirstTableRef.Accept(visitor);
            if (SecondTableRef != null)
                SecondTableRef.Accept(visitor);
            base.AcceptChildren(visitor);
        }
    }

    public partial class WQualifiedJoin : WJoinTableReference
    {
        internal QualifiedJoinType QualifiedJoinType { set; get; }
        internal WBooleanExpression JoinCondition { set; get; }
        internal JoinHint JoinHint { set; get; }

        internal override bool OneLine()
        {
            return FirstTableRef.OneLine() &&
                   SecondTableRef.OneLine() &&
                   JoinCondition.OneLine();
        }

        internal override string ToString(string indent)
        {
            var sb = new StringBuilder(32);

            sb.Append(FirstTableRef.ToString(indent)+"\n");

            sb.AppendFormat(" {1}{0}\n ", TsqlFragmentToString.JoinType(QualifiedJoinType, JoinHint), indent);

            //if (SecondTableRef.OneLine())
            //{
            //    sb.Append(SecondTableRef.ToString());
            //}
            //else
            //{
                //sb.Append("\r\n");
                sb.Append(SecondTableRef.ToString(indent));
            //}

            sb.Append("\n"+indent +"ON ");

            //if (JoinCondition.OneLine())
            //{
            //    sb.Append(JoinCondition.ToString());
            //}
            //else
            //{
                //sb.Append("\r\n");
                sb.Append(JoinCondition.ToString(""));
            //}

            return sb.ToString();
        }


        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (JoinCondition != null)
                JoinCondition.Accept(visitor);
            base.AcceptChildren(visitor);
        }
    }

    public partial class WUnqualifiedJoin : WJoinTableReference
    {
        public UnqualifiedJoinType UnqualifiedJoinType { get; set; }
        internal override bool OneLine()
        {
            return FirstTableRef.OneLine() &&
                   SecondTableRef.OneLine();
        }

        internal override string ToString(string indent)
        {
            var sb = new StringBuilder(32);

            sb.Append(FirstTableRef.ToString(indent)+"\n");

            sb.AppendFormat(" {1}\t{0} ", TsqlFragmentToString.JoinType(UnqualifiedJoinType),indent);

            //if (SecondTableRef.OneLine())
            //{
            //    sb.Append(SecondTableRef);
            //}
            //else
            {
                //sb.Append("\r\n");
                sb.Append(SecondTableRef.ToString(""));
            }

            return sb.ToString();
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }
    }

    public partial class WParenthesisTableReference : WTableReference
    {
        internal WTableReference Table { get; set; }

        internal override bool OneLine()
        {
            return Table.OneLine();
        }

        internal override string ToString(string indent)
        {
            //if (OneLine())
            //{
            //    return string.Format(CultureInfo.CurrentCulture, "{0}({1})", indent, Table.ToString(""));
            //}
            //return string.Format(CultureInfo.CurrentCulture, "{0}(\r\n{1})", indent, Table.ToString(indent + " "));
            return string.Format(CultureInfo.CurrentCulture, "{0}(\n{1}\n{0})", indent, Table.ToString(indent+"\t"));
        }

        internal override IList<string> TableAliases()
        {
            return Table.TableAliases();
        }

        public override void Accept(WSqlFragmentVisitor visitor)
        {
            if (visitor != null)
                visitor.Visit(this);
        }

        public override void AcceptChildren(WSqlFragmentVisitor visitor)
        {
            if (Table != null)
                Table.Accept(visitor);
            base.AcceptChildren(visitor);
        }
    }
}
