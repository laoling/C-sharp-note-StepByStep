本节笔记用于windows窗体程序的高级功能。

# Windows窗体的高级功能 #

我们知道一个Windows程序界面很少只包含一个对话框，这些应用程序一般使用单一文档界面（Single Document Interface，SDI）或多文档界面（Multiple Document Interface，MDI），这两种类型的应用程序通常会使用大量的菜单和工具栏，本章主要讨论它们。

本章首先介绍菜单控件，再介绍工具栏，说明如何把工具栏上的按钮和特定的菜单项链接起来，或者把特定的菜单项与工具栏上的按钮链接起来。接着创建SDI和MDI应用程序，主要讨论MDI应用程序，因为SDI基本上是MDI应用程序的子集。

我们前面接触的控件都是.NET Framework提供的控件。这些控件非常强大，提供了很多功能。但有时候它们还不够，所以需要创建定制的控件，本章最后就是介绍如何创建定义控件。

## 一、菜单和工具栏 ##

几乎每个Windows应用都包含菜单和工具栏。在为Windows操作系统写的程序中，菜单和工具栏可能是不可或缺的重要部分。为了帮助用户创建应用程序的菜单，VS提供了两个空间，使用它们不必做太多的工作，就可以快速创建外观类似于VS的菜单和工具栏。

### 1、两个实质一样的控件

下面介绍的这两个空间是VS2005的新增控件，它们为开发人员提供了很多强大的功能。使用这两个空间可以创建出具有工具栏和菜单的应用程序。以前需要几个星期才能完成创建的应用程序，现在变成区区数秒即可完成的简单任务。

我们要使用的控件包含在后缀为Strip的控件系列中，分别是ToolStrip、MenuStrip和StatusStrip。StatusStrip后面会详细介绍。从它们最纯粹的形式来看，ToolStrip和MenuStrip实际上是相同的控件，因为MenuStrip直接派生于ToolStrip。也就是说，ToolStrip能做的工作，MenuStrip也能完成。显然一起使用工作会完成的更好。

### 2、使用MenuStrip控件

除了MenuStrip控件之外，还有许多控件可以填充菜单。3个最常见的控件是ToolStripMenuItem、ToolStripDropDown和ToolStripSeparator。这些控件表示查看菜单或工具栏中的水平或垂直分割线。

除了MenuStrip外，还有一种菜单ContextMenuStrip。当用户右击一项时，关联菜单就会显示出来，它通常显示与该项相关的信息。

### 3、手工创建菜单

### 4、ToolTripMenuItem控件的其他属性

### 5、给菜单添加功能

## 二、工具栏 ##

### 1、ToolTrip控件的属性

### 2、ToolTrip的项

### 3、StatusStrip控件

### 4、StatusStripStatusLabel的属性

## 三、SDI和MDI应用程序 ##

## 四、生成MDI应用程序 ##

## 五、创建控件 ##

### 1、调试用户控件

### 2、扩展LabelTextbox控件