using System.Drawing.Imaging;

using System.Drawing;

namespace concatenate
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Write your first Image Path:");
            string imagePath1 = Console.ReadLine();
            Console.WriteLine("Write your second Image Path:");
            string imagePath2 = Console.ReadLine();

            try
            {
                Bitmap inputImage1 = new Bitmap(imagePath1);
                Bitmap inputImage2 = new Bitmap(imagePath2); 

                ConcatenationImage con = new ConcatenationImage();
                Console.WriteLine("Write Your Concatneation Type: vertical or horizontal");
                
                Bitmap outputImage;
                
                string concnatenationType = Console.ReadLine();
                
                if (concnatenationType.ToLower() == "vertical")
                {
                     outputImage = con.ConcatenateVertically(inputImage1, inputImage2);
                }

                else if(concnatenationType.ToLower() == "horizontal")
                {
                     outputImage = con.ConcatenateHorizontally(inputImage1, inputImage2);
                }
                else
                {
                    throw new InvalidOperationException("Invalid concatenation type provided");

                }
              

                string outputImageFileName = "outputttttt.jpg";
                string outputImagePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, outputImageFileName);
                outputImage.Save(outputImagePath, ImageFormat.Jpeg);

                Console.WriteLine($"Image concatenated successfully. Processed image saved as {outputImagePath}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex}");
            }
        }
    }
}