using Contexteer;
using Contexteer.Configuration;
using FeatureSwitcher.Configuration;
using FeatureSwitcher.Contexteer.Specs.Behaviors;
using FeatureSwitcher.Contexteer.Specs.Contexts;
using FeatureSwitcher.Contexteer.Specs.Domain;
using Machine.Specifications;

namespace FeatureSwitcher.Contexteer.Specs
{
    public class When_using_partial_configuration_and_contexts : WithCleanUp
    {
        Establish ctx = () =>
                            {
                                Features.Are.
                                    ConfiguredBy.Custom(EnableByName<Simple>.IsEnabled).And.
                                    NamedBy.Custom(Features.OfType<Simple>.NamedByTypeName);

                                In<Default>.Contexts.FeaturesAre().
                                    ConfiguredBy.Custom(EnableByName<Basic>.IsEnabled).And.
                                    NamedBy.Custom(Features.OfType<Basic>.NamedByTypeName);

                                In<BusinessBranch>.Contexts.FeaturesAre().
                                    ConfiguredBy.Custom(EnableByName<Complex>.IsEnabled).And.
                                    NamedBy.Custom(Features.OfType<Complex>.NamedByTypeName);
                            };

        Behaves_like<Disabled<Basic>> a_disabled_feature_basic;
        Behaves_like<EnabledInDefault<Basic>> an_enabled_feature_basic_in_default;
        Behaves_like<EnabledInHeadquaters<Basic>> an_enabled_feature_basic_in_headquarters;

        Behaves_like<Enabled<Simple>> an_enabled_feature_simple;
        Behaves_like<EnabledInDefault<Simple>> an_enabled_feature_simple_in_default;
        Behaves_like<EnabledInHeadquaters<Simple>> an_enabled_feature_simple_in_headquarters;

        Behaves_like<Disabled<Complex>> a_disabled_feature_complex;
        Behaves_like<DisabledInDefault<Complex>> an_enabled_feature_complex_in_default;
        Behaves_like<EnabledInHeadquaters<Complex>> an_enabled_feature_complex_in_headquarters;
    }
}