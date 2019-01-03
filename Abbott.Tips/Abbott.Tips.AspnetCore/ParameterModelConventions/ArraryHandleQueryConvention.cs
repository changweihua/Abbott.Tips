using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abbott.Tips.AspnetCore.ParameterModelConventions
{
    /// <summary>
    /// 《WebAPI Get请求参数传入输入带有[]不识别问题》
    /// 1.https://localhost:44390/api/values?status=1&status=2
    /// 2.https://localhost:44390/api/values?status[]=1&status[]=2
    /// 3.https://localhost:44390/api/values?status[0]=1&status[1]=2
    /// </summary>
    public class ArraryHandleQueryConvention : IParameterModelConvention
    {
        public void Apply(ParameterModel parameter)
        {
            //parameter.ParameterType.IsArray || 
            if (parameter.Attributes.OfType<FromQueryAttribute>().Any())
                parameter.Action.Filters.Add(new ArrayQueryStringAttribute(parameter.ParameterName));

            //parameter.Action.Filters.Add(new FormArrayAttribute(parameter.ParameterName));

            parameter.Action.Filters.Add(new FromFormArrayAttribute(parameter.ParameterName));

        }
    }

    #region From Form

    public class FromFormArrayValueProvider : FormValueProvider
    {
        private readonly string _key;
        private readonly IFormCollection _values;

        public FromFormArrayValueProvider(IFormCollection values)
            : this(null, values)
        {
        }

        public FromFormArrayValueProvider(string key, IFormCollection values)
            : base(BindingSource.Form, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key + "[]");

            if (_key != null && _key != key)
            {
                return result;
            }
            if (result != ValueProviderResult.None)
            {
                var splitValues = new StringValues(result.Values.ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class FromFormArrayAttribute : Attribute, IResourceFilter
    {
        private readonly FromFormArrayValueProviderFactory _factory;

        public FromFormArrayAttribute(string key)
        {
            _factory = new FromFormArrayValueProviderFactory();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }
    }

    public class FromFormArrayValueProviderFactory : IValueProviderFactory
    {
        private readonly string _key;

        public FromFormArrayValueProviderFactory()
        {
        }

        public FromFormArrayValueProviderFactory(string key)
        {
            _key = key;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new FromFormArrayValueProvider(_key, context.ActionContext.HttpContext.Request.Form));
            return Task.CompletedTask;
        }
    }

    #endregion

    #region From Body

    public class FormArrayValueProvider : FormValueProvider
    {
        private readonly string _key;
        private readonly IFormCollection _values;

        public FormArrayValueProvider(IFormCollection values)
            : this(null, values)
        {
        }

        public FormArrayValueProvider(string key, IFormCollection values)
            : base(BindingSource.Body, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key + "[]");

            if (_key != null && _key != key)
            {
                return result;
            }
            if (result != ValueProviderResult.None)
            {
                var splitValues = new StringValues(result.Values.ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class FormArrayAttribute : Attribute, IResourceFilter
    {
        private readonly FormArrayValueProviderFactory _factory;

        public FormArrayAttribute(string key)
        {
            _factory = new FormArrayValueProviderFactory();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }
    }

    public class FormArrayValueProviderFactory : IValueProviderFactory
    {
        private readonly string _key;

        public FormArrayValueProviderFactory()
        {
        }

        public FormArrayValueProviderFactory(string key)
        {
            _key = key;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new FormArrayValueProvider(_key, context.ActionContext.HttpContext.Request.Form));
            return Task.CompletedTask;
        }
    }

    #endregion

    #region Query String


    public class ArrayQueryStringValueProvider : QueryStringValueProvider
    {
        private readonly string _key;
        private readonly IQueryCollection _values;

        public ArrayQueryStringValueProvider(IQueryCollection values)
            : this(null, values)
        {
        }

        public ArrayQueryStringValueProvider(string key, IQueryCollection values)
            : base(BindingSource.Query, values, CultureInfo.InvariantCulture)
        {
            _key = key;
            _values = values;
        }

        public override ValueProviderResult GetValue(string key)
        {
            var result = base.GetValue(key + "[]");

            if (_key != null && _key != key)
            {
                return result;
            }
            if (result != ValueProviderResult.None)
            {
                var splitValues = new StringValues(result.Values.ToArray());
                return new ValueProviderResult(splitValues, result.Culture);
            }
            return result;
        }
    }

    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
    public class ArrayQueryStringAttribute : Attribute, IResourceFilter
    {
        private readonly ArrayQueryStringValueProviderFactory _factory;

        public ArrayQueryStringAttribute(string key)
        {
            _factory = new ArrayQueryStringValueProviderFactory();
        }

        public void OnResourceExecuted(ResourceExecutedContext context)
        {
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            context.ValueProviderFactories.Insert(0, _factory);
        }
    }

    public class ArrayQueryStringValueProviderFactory : IValueProviderFactory
    {
        private readonly string _key;

        public ArrayQueryStringValueProviderFactory()
        {
        }

        public ArrayQueryStringValueProviderFactory(string key)
        {
            _key = key;
        }

        public Task CreateValueProviderAsync(ValueProviderFactoryContext context)
        {
            context.ValueProviders.Insert(0, new ArrayQueryStringValueProvider(_key, context.ActionContext.HttpContext.Request.Query));
            return Task.CompletedTask;
        }
    }

    #endregion
}
