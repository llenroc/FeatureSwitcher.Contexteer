using Contexteer;
using FeatureSwitcher.Contexteer.Specs.Domain;
using Machine.Specifications;

namespace FeatureSwitcher.Contexteer.Specs.Behaviors
{
    [Behaviors]
    public class EnabledInDefault<TComponent> where TComponent : IComponent, new()
    {
        It should_be_enabled_as_generic = () => Feature<TComponent>.Is().EnabledInContextOf(Default.Context).ShouldBeTrue();

        It should_be_enabled_as_base_instance = () => ((IComponent)new TComponent()).Is().EnabledInContextOf(Default.Context).ShouldBeTrue();

        It should_be_enabled_as_instance = () => new TComponent().Is().EnabledInContextOf(Default.Context).ShouldBeTrue();

        It should_be_not_disabled_as_generic = () => Feature<TComponent>.Is().DisabledInContextOf(Default.Context).ShouldBeFalse();

        It should_be_not_disabled_as_base_instance = () => ((IComponent)new TComponent()).Is().DisabledInContextOf(Default.Context).ShouldBeFalse();

        It should_be_not_disabled_as_instance = () => new TComponent().Is().DisabledInContextOf(Default.Context).ShouldBeFalse();
    }
}