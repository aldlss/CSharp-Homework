using homework8.viewModel;

namespace homework8Test
{
    public class ViewModelTest
    {
        [Fact]
        public void DbPathTest()
        {
            Environment.SetEnvironmentVariable("WORDS_DB_PATH", "123");
            Assert.ThrowsAny<Exception>(() =>
            {
                var _ = new ViewModel();
            });

            Environment.SetEnvironmentVariable("WORDS_DB_PATH", "F:\\code\\homework\\softwareStructBase\\homework8\\words.db");
            var viewModel = new ViewModel();
            Assert.NotNull(viewModel);
        }
    }
}
