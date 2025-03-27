using System.Drawing;
using System.Drawing.Imaging;


namespace concatenate;

public class ConcatenationImage
{
    private Bitmap ConcatenateImages(Bitmap firstImage, Bitmap secondImage, bool isHorizontal)
    {
        int firstImageWidth = firstImage.Width;

        int firstImageHeight = firstImage.Height;
        
        int secondImageWidth = secondImage.Width;

        int secondImageHeight = secondImage.Height;
        
      
        if (firstImageHeight != secondImageHeight || firstImageWidth != secondImageWidth)
        {
            if (isHorizontal)
            {
                if (firstImageHeight > secondImageHeight)
                {
                    secondImage = ResizeImage(firstImage, secondImage, isHorizontal);
                
                    secondImageHeight = secondImage.Height;
                }

                else if(firstImageHeight > secondImageHeight)
                {
                    firstImage = ResizeImage(firstImage, secondImage, isHorizontal);
                
                    firstImageHeight = firstImage.Height;
                
                }
            }

            if (!isHorizontal)
            {
                 if (firstImageWidth > secondImageWidth)
                {
                    secondImage = ResizeImage(firstImage, secondImage, isHorizontal);
                
                    secondImageWidth = secondImage.Width;
                    Console.WriteLine(secondImageWidth);
                }
                else if (firstImageWidth < secondImageWidth)
                {
                    firstImage = ResizeImage(firstImage, secondImage, isHorizontal);
                
                    firstImageWidth = firstImage.Width;
                
                }

            }
 
            

        }
       
        

        int width = isHorizontal ? firstImageWidth + secondImageWidth : Math.Max(firstImageWidth, secondImageWidth);
        int height = isHorizontal ? Math.Max(firstImageHeight, secondImageHeight): firstImageHeight + secondImageHeight;

        Bitmap resultImage = new Bitmap(width, height, PixelFormat.Format8bppIndexed);
        
        ColorPalette resultPalette = resultImage.Palette;
        ColorPalette sourcePalette = firstImage.Palette;
        for (int i = 0; i < sourcePalette.Entries.Length; i++)
        {
            resultPalette.Entries[i] = sourcePalette.Entries[i];
        }
        resultImage.Palette = resultPalette;
        
        BitmapData firstImageData = firstImage.LockBits(new Rectangle(0, 0, firstImageWidth, firstImageHeight), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
        BitmapData secondImageData = secondImage.LockBits(new Rectangle(0, 0, secondImageWidth, secondImageHeight), ImageLockMode.ReadOnly, PixelFormat.Format8bppIndexed);
        BitmapData resultImageData = resultImage.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, PixelFormat.Format8bppIndexed);

        CopyPixels(resultImageData, firstImageData, secondImageData, isHorizontal);

        firstImage.UnlockBits(firstImageData);
        
        secondImage.UnlockBits(secondImageData);
        
        resultImage.UnlockBits(resultImageData);
        
        return resultImage;

        
    }
    
    private void CopyPixels(BitmapData resultImageData, BitmapData firstImageData,BitmapData secondImageData, bool isHorizontal)
    {
        int firstHeight = firstImageData.Height;

        int firstWidth = firstImageData.Width;

        int secondWidth = secondImageData.Width;
        
        int secondHeight = secondImageData.Height;

        int resultWidth = resultImageData.Width;
        
        int FirstImageStride = firstImageData.Stride;
        int SecondImageStride = secondImageData.Stride;
        int ResultImageStride = resultImageData.Stride;
        int FirstImageOffset = FirstImageStride - firstImageData.Width;
        int SecondImageOffset = SecondImageStride - secondImageData.Width;
        int ResultImageOffset = ResultImageStride - resultImageData.Width;

        IntPtr fscan0 = firstImageData.Scan0;
            
        IntPtr sscan0 = secondImageData.Scan0;
            
        IntPtr rscan0 = resultImageData.Scan0;


        unsafe
        {
            byte* p1 = (byte*)(void*)fscan0;
            byte* p2 = (byte*)(void*)sscan0; 
            byte* pr = (byte*)(void*)rscan0;
            
        

            for (int y = 0; y < firstHeight; y++)
            {
                for (int x = 0; x < firstWidth; x++)
                {
                    pr[0] = p1[0];

                    ++p1;
                    ++pr;
                }

                p1 += FirstImageOffset;

                if (isHorizontal)
                {
                    int FirstIncrease = FirstImageOffset + secondWidth;

                    pr += FirstIncrease;

                }

                else
                {
                    pr += ResultImageOffset;
                }
       

            }
            
         
        
            
            

            if (isHorizontal)
            {
                pr = (byte*)(void*)rscan0;
                
                int seconImage = resultWidth / 2;

                pr += seconImage;
                
            }
            
            
            
                

            for (int y = 0; y < secondHeight; y++)
            {
                for (int x = 0; x < secondWidth; x++)
                {
                    pr[0] = p2[0];

                    ++p2;
                    ++pr;
                }
                    
                p2 += SecondImageOffset;
                if (isHorizontal)
                {
                    int SecondIncrease =  firstWidth + ResultImageOffset;
                    pr += SecondIncrease;
                    
                }
                else
                {
                    pr += ResultImageOffset;
                }
               
            }
        }
     
    }

    private Bitmap ResizeImage(Bitmap firstImage, Bitmap secondImage, bool isHorizontal)
    {
        int firstImageHeight = firstImage.Height;

        int firstImageWidth = firstImage.Width;

        int secondImageHeight = secondImage.Height;

        int secondImageWidth = secondImage.Width;
        
        BitmapData firstImageData = firstImage.LockBits(new Rectangle(0, 0, firstImageWidth, firstImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
        
        BitmapData secondImageData = secondImage.LockBits(new Rectangle(0, 0, secondImageWidth, secondImageHeight),
            ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);
        
        int FirstImageStride = firstImageData.Stride;
        int SecondImageStride = secondImageData.Stride;
        

        int targetImageHeight;

        int targetImageWidth;

        int targetImageOffset;

        IntPtr tscan0;

        
            Bitmap tempImage;
            if (isHorizontal)
            {
                if (firstImageHeight > secondImageHeight)
                {
                    targetImageHeight = secondImageHeight;
                
                    targetImageWidth = secondImageWidth;
                 
                    targetImageOffset = SecondImageStride - secondImageData.Width;
                 
                    tscan0 = secondImageData.Scan0;
                
                    tempImage = new Bitmap(secondImageWidth, firstImageHeight, PixelFormat.Format8bppIndexed);
                }
                else 
                {
                    targetImageHeight = firstImageHeight;
                
                    targetImageWidth = firstImageWidth;
                 
                    targetImageOffset = FirstImageStride - firstImageData.Width;

                 
                    tscan0 = firstImageData.Scan0;

                
                    tempImage = new Bitmap(firstImageWidth, secondImageHeight, PixelFormat.Format8bppIndexed);
                }  
            }
            else
            {
                 if (firstImageWidth < secondImageWidth)
                {
                    targetImageHeight = firstImageHeight;
                
                    targetImageWidth = firstImageWidth;
                 
                    targetImageOffset = FirstImageStride - firstImageData.Width;

                 
                    tscan0 = firstImageData.Scan0;

                
                    tempImage = new Bitmap(secondImageWidth, firstImageHeight, PixelFormat.Format8bppIndexed);
                
                }
                else
                {
                    targetImageHeight = secondImageHeight;
                
                    targetImageWidth = secondImageWidth;
                 
                    targetImageOffset = SecondImageStride - secondImageData.Width;

                 
                    tscan0 = secondImageData.Scan0;

                
                    tempImage = new Bitmap(firstImageWidth, secondImageHeight, PixelFormat.Format8bppIndexed);
                }
            }
          

            int tempImageWidth = tempImage.Width;

            int tempImageHeight = tempImage.Height;
            

                ColorPalette resultPalette = tempImage.Palette;
                ColorPalette sourcePalette = secondImage.Palette;
                for (int i = 0; i < sourcePalette.Entries.Length; i++)
                {
                    resultPalette.Entries[i] = sourcePalette.Entries[i];
                }
                
                tempImage.Palette = resultPalette;
                
                BitmapData tempImageData = tempImage.LockBits(new Rectangle(0, 0, tempImageWidth, tempImageHeight), ImageLockMode.ReadWrite, PixelFormat.Format8bppIndexed);

                
                int tempImageStride = tempImageData.Stride;
                
                int tempImageOffset = tempImageStride - tempImageWidth;
                
                IntPtr tescan0 = tempImageData.Scan0;

                unsafe
                {
                    byte* targetImagePointer = (byte*)(void*)tscan0;
                    
                    byte* tempImagePointer = (byte*)(void*)tescan0;
                    
                    for (int y = 0; y < tempImageHeight; y++)
                    {
                        for (int x = 0; x < tempImageWidth; x++)
                        {
                            
                            if (x < targetImageWidth && y < targetImageHeight)
                            {
                                tempImagePointer[0] = targetImagePointer[0];
                                
                                ++targetImagePointer;
                            }
                            
                            else
                            {
                                tempImagePointer[0] = 255; 
                            }
                         

                            ++tempImagePointer;


                        }

                        if (y < targetImageHeight)
                            targetImagePointer += targetImageOffset;

                        tempImagePointer += tempImageOffset;
                        
                    }
                    
                    
                }
                
                tempImage.UnlockBits(tempImageData);
                
                firstImage.UnlockBits(firstImageData);
                
                secondImage.UnlockBits(secondImageData);

                return tempImage;
                

    }


    public Bitmap ConcatenateHorizontally(Bitmap firstImage, Bitmap secondImage)
    {
        return ConcatenateImages(firstImage, secondImage, true);
    }

    public Bitmap ConcatenateVertically(Bitmap firstImage, Bitmap secondImage)
    {
        return ConcatenateImages(firstImage, secondImage, false);
    }
}