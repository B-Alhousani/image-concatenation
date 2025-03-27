# Image Concatenation Tool (C# Console App)

This C# console application allows users to concatenate two grayscale images either **horizontally** or **vertically**. It ensures the input images are properly resized and aligned for seamless merging.

## Features

- Supports vertical and horizontal image concatenation
- Automatically resizes mismatched dimensions
- Processes 8-bit grayscale `.jpg` images using `System.Drawing`
- Saves the concatenated result to a file

## Project Structure

- `Program.cs`  
  Handles user interaction and calls the image concatenation logic.

- `ConcatenationImage.cs`  
  Contains all the logic for resizing, aligning, and combining bitmap images.

## How to Run

1. Open the project in Visual Studio or any C# IDE.
2. Build and run the project.
3. When prompted:
   - Enter the full path for the **first image**
   - Enter the full path for the **second image**
   - Specify the **concatenation type**: `horizontal` or `vertical`
4. The result will be saved as `outputttttt.jpg` in the application's directory.

## Notes

- Only **grayscale 8-bit images** (`PixelFormat.Format8bppIndexed`) are supported.
- Output uses the palette of the first image.
- Images with different dimensions will be resized while preserving their grayscale content.

## Example

```
Write your first Image Path:
C:\images\img1.jpg
Write your second Image Path:
C:\images\img2.jpg
Write Your Concatneation Type: vertical or horizontal
horizontal
Image concatenated successfully. Processed image saved as C:\your_app_path\outputttttt.jpg
```
Developed by Basil Al Housani
