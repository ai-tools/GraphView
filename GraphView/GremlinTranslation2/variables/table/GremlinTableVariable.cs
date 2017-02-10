﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace GraphView
{
    internal abstract class GremlinTableVariable : GremlinVariable
    {
        public virtual void test1() { }

        public WEdgeType EdgeType { get; set; }
        public GremlinVariableType VariableType { get; set; }

        public GremlinTableVariable(GremlinVariableType variableType)
        {
            SetVariableTypeAndGenerateName(variableType);
        }

        public GremlinTableVariable()
        {
            SetVariableTypeAndGenerateName(GremlinVariableType.Table);
        }

        internal override WEdgeType GetEdgeType()
        {
            if (EdgeType == null)
            {
                throw new QueryCompilationException("EdgeType can't be null");
            }
            return EdgeType;
        }

        public void SetVariableTypeAndGenerateName(GremlinVariableType variableType)
        {
            VariableType = variableType;
            VariableName = GremlinUtil.GenerateTableAlias(VariableType);
        }

        internal override void Populate(string property)
        {
            switch (GetVariableType())
            {
                case GremlinVariableType.Vertex:
                    if (GremlinUtil.IsEdgeProperty(property)) return;
                    break;
                case GremlinVariableType.Edge:
                    if (GremlinUtil.IsVertexProperty(property)) return;
                    break;
                case GremlinVariableType.Scalar:
                    if (property != GremlinKeyword.ScalarValue) return;
                    break;
                case GremlinVariableType.Property:
                    if (property != GremlinKeyword.PropertyValue) return;
                    break;
            }
            base.Populate(property);
        }

        public virtual WTableReference ToTableReference()
        {
            throw new NotImplementedException();
        }

        internal override GremlinVariableProperty DefaultVariableProperty()
        {
            switch (VariableType)
            {
                case GremlinVariableType.Edge:
                    return GetVariableProperty(GremlinKeyword.EdgeID);
                case GremlinVariableType.Scalar:
                    return GetVariableProperty(GremlinKeyword.ScalarValue);
                case GremlinVariableType.Vertex:
                    return GetVariableProperty(GremlinKeyword.NodeID);
                case GremlinVariableType.Property:
                    return GetVariableProperty(GremlinKeyword.PropertyValue);
            }
            return new GremlinVariableProperty(this, GremlinKeyword.TableDefaultColumnName);
        }

        internal override GremlinVariableProperty DefaultProjection()
        {
            switch (VariableType)
            {
                case GremlinVariableType.Edge:
                    return GetVariableProperty(GremlinKeyword.Star);
                case GremlinVariableType.Scalar:
                    return GetVariableProperty(GremlinKeyword.ScalarValue);
                case GremlinVariableType.Vertex:
                    return GetVariableProperty(GremlinKeyword.Star);
                case GremlinVariableType.Property:
                    return GetVariableProperty(GremlinKeyword.PropertyValue);
            }
            return new GremlinVariableProperty(this, GremlinKeyword.TableDefaultColumnName);
        }

        internal override GremlinVariableType GetVariableType()
        {
            return VariableType;
        }

        internal override void Range(GremlinToSqlContext currentContext, int low, int high)
        {
            Low = low;
            High = high;
        }
    }

    internal abstract class GremlinScalarTableVariable : GremlinTableVariable
    {
        public GremlinScalarTableVariable(): base(GremlinVariableType.Scalar) {}
    }

    internal abstract class GremlinVertexTableVariable : GremlinTableVariable
    {
        public GremlinVertexTableVariable(): base(GremlinVariableType.Vertex) {}

        internal override void Both(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }

        internal override void BothE(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }

        internal override void BothV(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void In(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }

        internal override void InE(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }

        internal override void Out(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }

        internal override void OutE(GremlinToSqlContext currentContext, List<string> edgeLabels)
        {
            throw new NotImplementedException();
        }
        internal override void Drop(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string propertyKey, object value)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string label, string propertyKey, object value)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string propertyKey, Predicate predicate)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string label, string propertyKey, Predicate predicate)
        {
            throw new NotImplementedException();
        }

        internal override void HasId(GremlinToSqlContext currentContext, List<object> values)
        {
            throw new NotImplementedException();
        }

        internal override void HasLabel(GremlinToSqlContext currentContext, List<object> values)
        {
            throw new NotImplementedException();
        }

        internal override void Properties(GremlinToSqlContext currentContext, List<string> propertyKeys)
        {
            throw new NotImplementedException();
        }

        internal override void Values(GremlinToSqlContext currentContext, List<string> propertyKeys)
        {
            throw new NotImplementedException();
        }
    }

    internal abstract class GremlinEdgeTableVariable : GremlinTableVariable
    {
        public GremlinEdgeTableVariable(): base(GremlinVariableType.Edge) {}

        internal override void InV(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void OutV(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void OtherV(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void Drop(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string propertyKey, object value)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string label, string propertyKey, object value)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string propertyKey, Predicate predicate)
        {
            throw new NotImplementedException();
        }

        internal override void Has(GremlinToSqlContext currentContext, string label, string propertyKey, Predicate predicate)
        {
            throw new NotImplementedException();
        }

        internal override void HasId(GremlinToSqlContext currentContext, List<object> values)
        {
            throw new NotImplementedException();
        }

        internal override void HasLabel(GremlinToSqlContext currentContext, List<object> values)
        {
            throw new NotImplementedException();
        }
    }

    internal abstract class GremlinPropertyTableVariable : GremlinTableVariable
    {
        public GremlinPropertyTableVariable(): base(GremlinVariableType.Property) { }

        internal override void Key(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void Value(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void Drop(GremlinToSqlContext currentContext)
        {
            throw new NotImplementedException();
        }

        internal override void HasKey(GremlinToSqlContext currentContext, List<string> values)
        {
            throw new NotImplementedException();
        }

        internal override void HasValue(GremlinToSqlContext currentContext, List<object> values)
        {
            throw new NotImplementedException();
        }
    }

    internal abstract class GremlinDropVariable : GremlinTableVariable
    {
        public GremlinDropVariable() : base(GremlinVariableType.NULL) {}

        internal override GremlinVariableType GetVariableType()
        {
            return GremlinVariableType.NULL;
        }
    }
}
