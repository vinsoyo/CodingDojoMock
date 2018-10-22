namespace MIAM
{
    public class ContextFactory
    {
        public ContextFactory()
        {
        }

        public virtual IMiamDbContext CreateContext()
        {
            return new MiamDbContext();
        }
    }
}