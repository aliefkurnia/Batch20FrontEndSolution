namespace BlazorWepApp.Pages
{
    public partial class Counter
    {
    
    private int currentCount = 0;

        private void IncrementCount()
        {
            currentCount++;
        }

        private void Decrement()
        {
            if (currentCount > 0)
            {
                currentCount--;

            }

        }
    }

}

