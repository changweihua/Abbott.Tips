using System;

namespace Abbott.Tips.Framework.Dependency
{
    /// <summary>
    /// 自动接口
    /// </summary>
    /// <remarks>
    /// 用于Autofac自动注册时，查找所有依赖该接口的实现。
    /// 实现自动注册功能
    /// </remarks>
    public interface IDependency
    {

    }

    /// <summary>
    /// 单例接口
    /// </summary>
    /// <remarks>
    /// 用于Autofac自动注册时，查找所有依赖该接口的实现。
    /// 实现自动注册功能
    /// </remarks>
    public interface ISingletonDependency
    {
    }

    /// <summary>
    /// 所有接口的依赖接口，每次创建新实例
    /// </summary>
    /// <remarks>
    /// 用于Autofac自动注册时，查找所有依赖该接口的实现。
    /// 实现自动注册功能
    /// </remarks>
    public interface ITransientDependency
    {
    }
}
