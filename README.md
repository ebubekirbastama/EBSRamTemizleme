
# EBSRamTemizleme

EBSRamTemizleme is a modern Windows Forms application designed to monitor and clear RAM usage periodically. The application offers a sleek UI with gradient background, rounded corners, and custom shadows, creating a visually appealing user experience. It also supports draggable functionality and runs in the background without a taskbar icon.

---

## Features

- **RAM Monitoring**: Displays current RAM usage in gigabytes.
- **RAM Cleaning**: Automatically clears RAM at regular intervals using the `BekraRamclear` library.
- **Customizable Design**: Includes rounded corners, gradient backgrounds, and shadow effects for a modern look.
- **Draggable Window**: Allows easy repositioning of the window by dragging.
- **Keyboard Shortcuts**: 
  - Press `ESC` to close the application.

---

## Prerequisites

- **.NET Framework**: Ensure the required version of the .NET Framework is installed on your system.
- **BekraRamclear Library**: The application uses the `BekraRamclear` library for RAM cleaning. Ensure it is included in the project references.

---

## Installation

1. Clone or download the repository.
2. Open the project in Visual Studio or your preferred IDE.
3. Build the solution to ensure all dependencies are resolved.
4. Run the application.

---

## Usage

1. Launch the application.
2. The window will automatically position itself at the top-right corner of the screen.
3. View the RAM usage information displayed on the interface.
4. The application will clean the RAM every 5 seconds automatically.
5. Drag the window to reposition it if necessary.

---

## Customization

You can customize various aspects of the application:
- **Interval Timing**: Modify the `timer.Interval` value in the `Form1_Load` method to change the RAM cleaning frequency.
- **UI Appearance**: Adjust gradient colors, border radius, and shadow settings in the `ModernForm_Paint` method.
- **Keyboard Shortcuts**: Add or modify key handling in the `Form1_KeyDown` method.

---

## License

This project is licensed under the Apache-2.0 License. See the [LICENSE](LICENSE) file for more details.

---

## Contributing

If you have suggestions or want to contribute:
1. Fork the repository.
2. Create a new branch for your changes.
3. Submit a pull request for review.

---

## Contact

For any inquiries or issues, feel free to reach out:

- **Author**: Ebubekir Bastama  
- **Website**: [csharpegitimi.com.tr](https://csharpegitimi.com.tr)  
- **Twitter**: [@ebubekirstt](https://twitter.com/ebubekirstt)  

Enjoy efficient memory management with **EBSRamTemizleme**! 🎉
