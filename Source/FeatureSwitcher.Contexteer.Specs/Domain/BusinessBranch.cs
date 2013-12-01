using Contexteer;

namespace FeatureSwitcher.Contexteer.Specs.Domain
{
    public class BusinessBranch : IContext
    {
        public static readonly BusinessBranch Headquarters = new BusinessBranch();

        public static readonly BusinessBranch BranchOffice = new BusinessBranch();
    }
}