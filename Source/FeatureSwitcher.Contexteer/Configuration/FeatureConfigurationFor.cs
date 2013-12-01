using System;
using System.Linq;
using Contexteer;

namespace FeatureSwitcher.Configuration
{
    internal class FeatureConfigurationFor<TContext> : IConfigureFeaturesFor<TContext>, IConfigureBehaviorFor<TContext>, IConfigureNamingFor<TContext>
        where TContext : IContext
    {
        private Func<TContext, Feature.NamingConvention[]> _namingConvention;
        private Func<TContext, Feature.NamingConvention[]> NamingConvention
        {
            get { return _namingConvention ?? (ctx => null); }
        }
        private Func<TContext, Feature.Behavior[]> _behavior;
        private Func<TContext, Feature.Behavior[]> Behavior
        {
            get { return _behavior ?? (ctx => null); }
        }

        public IConfigureFeatures And
        {
            get { return this; }
        }

        IConfigureNamingFor<TContext> IConfigureFeaturesFor<TContext>.NamedBy
        {
            get { return this; }
        }

        IConfigureBehaviorFor<TContext> IConfigureFeaturesFor<TContext>.ConfiguredBy
        {
            get { return this; }
        }

        IConfigureFeaturesFor<TContext> IConfigureFeaturesFor<TContext>.And
        {
            get { return this; }
        }

        IConfigureNaming IConfigureFeatures.NamedBy
        {
            get { return this; }
        }

        IConfigureBehavior IConfigureFeatures.ConfiguredBy
        {
            get { return this; }
        }

        IConfigureFeaturesFor<TContext> IConfigureNamingFor<TContext>.Custom(Func<TContext, Feature.NamingConvention[]> namingConventions)
        {
            _namingConvention = namingConventions;
            return this;
        }

        IConfigureFeaturesFor<TContext> IConfigureBehaviorFor<TContext>.Custom(Func<TContext, Feature.Behavior[]> behaviors)
        {
            _behavior = behaviors;
            return this;
        }

        IConfigureFeatures IConfigureNaming.Custom(params Feature.NamingConvention[] namingConventions)
        {
            return (this as IConfigureNamingFor<TContext>).Custom(ctx => namingConventions);
        }

        IConfigureFeatures IConfigureBehavior.Custom(params Feature.Behavior[] behaviors)
        {
            return (this as IConfigureBehaviorFor<TContext>).Custom(ctx => behaviors);
        }

        public Feature.Configuration For(TContext context)
        {
            return new Feature.Configuration(
                type => (NamingConvention(context) ?? new Feature.NamingConvention[0]).Where(x => x != null).Select(x => x(type)).FirstOrDefault(x => x != null),
                name => (Behavior(context) ?? new Feature.Behavior[0]).Select(x => x(name)).FirstOrDefault(x => x.HasValue),
                typeof(TContext) != typeof(Default) ? FeatureConfiguration.For(Default.Context) : Feature.Configuration.Current);
        }
    }
}