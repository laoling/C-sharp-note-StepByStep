我们这节笔记记录WPF中关于布局的基础知识。

# 布局 #

在任意用户界面设计汇总，有一半的工作是以富有吸引力、灵活实用的方式组织内容。但真正的挑战是确保界面布局能够恰到好处地适应不同得窗口尺寸。

WPF用不同的容器（container）安排布局。每个容器有各自的布局逻辑——有些容器以堆栈方式布置元素，另一些容器在网格中不可见的单元格中排列元素，等等。在WPF中非常抵制基于坐标的布局，而是注重创建灵活的布局，使布局能够适应内部的内容的变化、不同的语言以及各种窗口尺寸。迁移到WPF的许多开发人员会觉得新布局系统令人倍感惊奇。

这里将介绍WPF布局模型的工作原理，并且将开始使用基本的布局容器。

## 一、理解WPF中的布局 ##

在Windows开发人员设计用户界面的方式上，WPF布局模型是一个重大改进。在WPF问世之前，Windows开发人员使用刻板的基于坐标的布局将控件放到正确位置。在WPF中，这种方式虽然也可行，但已经极少使用。大多数应用程序将使用类似于Web的flow布局；在使用流式布局模型时，控件可以扩大，并将其他控件挤到其他位置，开发人员能创建与显示分辨率和窗口大小无关的、在不同的显示器上正确缩放的用户界面；当窗口内容变化时，界面可调整自身，并且可以自如地处理语言的切换。要利用该系统的优势，首先需要进一步理解WPF布局模型的基本概念和假设。

#### 1.1 WPF布局原则

WPF窗口只能包含单个元素。为在WPF窗口中放置多个元素并创建更贴近实用的用户界面，需要在窗口上放置一个容器，然后在这个容器中添加其他元素。

在WPF中，布局由您使用的容器决定。尽管有多个容器可供选择，但理想的WPF窗口需要遵循下面几条重要原则：

* 不应显示设定元素的尺寸。元素应当可以改变尺寸以适合它们的内容。
* 不应使用屏幕坐标指定元素的位置。元素应当由它们的容器根据它们的尺寸、顺序以及其他特定于具体布局容器的信息进行排列。如果需要在元素之间添加空白空间，可使用Margin属性。
* 布局容器的子元素共享可用的空间。如果空间允许，布局容器会根据每个元素的内容尽可能为元素设置更合理的尺寸。它们还会向一个或多个子元素分配多余的空间。
* 可嵌套的布局容器。典型的用户界面使用Grid面板作为开始，Grid面板是WPF中功能最强大的容器，Grid面板可包含其他布局容器，包含的这些容器以更小的分组排列元素。

尽管对于这几条原则而言也有一些例外，但它们反映了WPF的总体设计目标。换句话说，如果创建WPF应用程序时遵循了这些原则，将会创建出更好的、更灵活的用户界面。如果不遵循这些原则，最终将得不到很适合WPF的并且难以维护的用户界面。

#### 1.2 布局过程

WPF布局包括两个阶段：测量阶段（measure）和排列阶段（arrange）。在测量阶段，容器遍历所有子元素，并询问子元素它们所期望的尺寸。在排列阶段，容器在合适的位置放置子元素。

当然，元素未必总能得到最合适的尺寸——有时容器没有足够大的空间以适应所含的元素。在这种情况下，容器为了适应可视化区域的尺寸，就必须剪裁不能满足要求的元素。在后面可看到，通常可通过设置最小窗口尺寸来避免这种情况。

#### 1.3 布局容器

所有WPF布局容器都是派生自System.Windows.Controls.Panel抽象类的面板。Panel类添加了少量成员，包括三个公有属性，下面列出了三个公有属性的详情：

名称 | 说明
:---:|:---:
Background | 该属性是用于为面板背景着色的画刷。如果想接收鼠标事件，就必须将该属性设置为非空值。
Children | 该属性是在面板中存储的条目集合。这是第一级条目——换句话说，这些条目自身也可以包含更多的条目。
IsItemsHost | 该属性是一个布尔值，如果面板用于显示与ItemsControl控件关联的项，该属性值为true。在大多数情况下，甚至不需要知道列表控件使用后台面板来管理它所包含的条目的布局。但如果希望创建自定义的列表，以不同方式放置子元素，该细节就变得很重要了。

就Panel基类本身而言没有什么特别的，但它是其他更多特性类的起点。WPF提供了大量可用于安排布局的继承自Panel的类，下表列出了其中几个最基本的类。与所有WPF控件和大多数可视化元素一样，这些类位于System.Windows.Controls名称空间中。

名称 | 说明
:---:|:---:
StackPanel | 在水平或垂直的堆栈中放置元素。这个布局容器通常用于更大、更复杂窗口中的一些小区域
WrapPanel | 在一系列可换行的行中放置元素。在水平方向上，WrapPanel面板从左向右放置条目，然后再随后的行中放置元素。在垂直方向上，WrapPanel面板在自上面下的列中放置元素，并使用附加的列放置剩余的条目
DockPanel | 根据容器的整个边界调整元素
Grid | 根据不可见的表格在行和列中排列元素，这是最灵活、最常见的容器之一
UniformGrid | 在不可见但是强制所有单元格具有相同尺寸的表中放置元素，这个布局容器不常用
Canvas | 使用固定坐标绝对定位元素。这个布局容器与传统Windows窗体应用程序最相似，但没有提供锚定或停靠功能，因此，对于尺寸可变的窗口，该布局容器不是合适的选择。如果选择的话，需要另外做一些工作

除这些核心容器外，还有几个更专业的面板，在各种控件中都可能遇到它们。这些容器包括专门用于包含特定控件子元素的面板——如TabPanel面板、ToolbarPanel面板以及ToolbarOverflowPanel面板。还有VirtualizingStackPanel面板，数据绑定列表控件使用该面板以大幅降低开销；还有InkCanvas控件，该控件和Canvas控件类似，但该控件支持处理平板电脑上的手写输入。

## 二、使用StackPanel面板进行简单布局 ##

StackPanel面板是最简单的布局容器之一。该面板简单地单行或单列中以堆栈形式放置其子元素。

例如包含四个按钮的窗口：

```xml
<Window x:Class="Layout.SimpleStack"
  xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
  xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
  Title="Layout" Height="223" Width="354">
  <StackPanel>
	<Label>A Button Stack</Label>
	<Button>Button 1</Button>
	<Button>Button 2</Button>
	<Button>Button 3</Button>
	<Button>Button 4</Button>
  </StackPanel>
</Window>
```

默认情况下，StackPanel面板按自上而下的顺序排列元素，使每个元素的高度适合它的内容。在这个示例中，这意味着标签和按钮的大小刚好足够适应它们内部包含的文本。所有元素都被拉伸到StackPanel面板的整个宽度，这也是窗口的宽度。如果加宽窗口，StackPanel面板也会变宽，并且按钮也会拉伸自身以适应变化。

通过设置Orientation属性，StackPanel面板也可用于水平排列元素：

```xml
<StackPanel Orientation="Horizontal">
```

现在元素指定它们的最小宽度并拉伸至容器面板的整个高度。根据窗口的当前大小，这可能导致一些元素不适应。

显然，这并未提供实际应用程序所需的灵活性。幸运的是，可使用布局属性对StackPanel面板和其他布局容器的工作方式进行精细调整，稍后会介绍。

#### 2.1 布局属性

尽管布局由容器决定，但子元素仍有一定的决定权。实际上，布局面板支持一小组布局属性，以便与子元素结合使用，下表列出了这些布局属性。

名称 | 说明
:---:|:---:
HorizontalAlignment | 当水平方向上有额外的空间时，该属性决定了子元素在布局容器中如何定位。可选用Center、Left、Right或Stretch等属性值
VerticalAlignment | 当垂直方向上有额外的空间时，该属性决定了子元素在布局容器中如何定位。可选用Center、Top、Buttom或Stretch等属性值
Margin | 该属性用于在元素的周围添加一定的空间。Margin属性是System.Windows.Thickness结构的一个实例。该结构具有分别用于为顶部、底部、左边和右边添加空间的独立组件
MinWidth和MinHeight | 这两个属性用于设置元素的最小尺寸。如果一个元素对于其他容器来说太大，该元素将被剪裁以适合容器
MaxWidth和MaxHeight | 这两个属性用于设置元素的最大尺寸。如果有更多可以使用的空间，那么在扩展子元素时就不会超出这一限制，即使将HorizontalAlignment和VerticalAlignment属性设置为Stretch也同样如此
Width和Height | 这两个属性用于显式的设置元素的尺寸。这一设置会重写为HorizontalAlignment和VerticalAlignment属性设置的Stretch值。但不能超出MinWidth、MinHight、MaxWidth和MaxHeight属性设置的范围

所有这些属性都是从FrameworkElement基类继承而来，所以在WPF窗口中可使用的所有图形小组件都支持这些属性。

这个属性列表就像它所没有包含的属性一样值得注意。如果查找熟悉的与位置相关的属性，例如top属性、right属性以及Location属性，是不会找到它们的。这是因为大多数布局容器都使用自动布局，并未提供显式定位元素的能力。

#### 2.2 对齐方式

为理解这些属性的工作原理，可进一步分析StackPanel面板的示例SimpleStack。

通常，对于Label控件，HorizontalAlignment属性的值默认为Left；对于Button控件，HorizontalAlignment属性的值默认为Stretch。这也是为什么每个按钮的宽度被调整为整列的宽度的原因所在。但可以改变这些细节：

```xml
<StackPanel>
   <Label HorizontalAlignment="Center">A Button Stack</Label>
   <Button HorizontalAlignment="Left">Button 1</Button>
   <Button HorizontalAlignment="Right">Button 2</Button>
   <Button>Button 3</Button>
   <Button>Button 4</Button>
</StackPanel>
```

这样，显式结果前面两个按钮的尺寸是它们应当具有的最小尺寸，并进行了对齐。而底部的两个按钮被拉伸至整个StackPanel面板的高度。如果改变窗口的尺寸，就会发现标签保持在中间位置，而前面两个按钮分别被粘贴到两边。

#### 2.3 边距

在SimpleStack示例中，当前情况下存在一个明显的问题。设计良好的窗口不只是包含元素——还应在元素之间包含一定的额外空间。为了添加额外的空间并使StackPanel面板中的按钮不那么紧密，可为控件设置边距。

当设置边距时，可为所有边设置相同的宽度，如下所示：

```xml
<Button Margin="5">Button 3</Button>
```

相应的，也可为控件的每个边以左、上、右、下的顺序设置不同的边距：

```xml
<Button Margin="5,10,5,10">Button 3</Button>
```

在代码中，使用Thickness结构来设置边距：

```csharp
cmd.Margin = new Thickness(5);
```

#### 2.4 最小尺寸、最大尺寸以及显示的设置尺寸

最后，每个元素都提供了Height和Width属性，用于显式地指定元素大小。但这种设置一般不是一个好主意。相反，如有必要，应当使用最大尺寸和最小尺寸属性，将控件限制在正确范围内。

例如，SimpleStack示例中你可能决定拉伸StackPanel容器中的按钮，使其适合StackPanel，但其宽度不能超过200单位，也不能小于100单位（默认情况下最初按钮的最小宽度是75单位）。下面是所需的标记：

```xml
<StackPanel Margin="3">
   <Label Margin="3" HorizontalAlignment="Center">A Button Stack</Label>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 1</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 2</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 3</Button>
   <Button Margin="3" MaxWidth="200" MinWidth="100">Button 4</Button>
</StackPanel>
```

当StackPanel调整按钮的尺寸时，需要考虑以下几部分信息：

* **最小尺寸。**每个按钮的尺寸始终不能小于最小尺寸。
* **最大尺寸。**每个按钮的尺寸始终不能超过最大尺寸。
* **内容。**如果按钮中的内容需要更大的宽度，StackPanel容器会尝试扩展按钮。
* **容器尺寸。**如果最小宽度大于StackPanel面板的宽度，按钮的一部分将被剪裁掉。否则，不允许按钮比StackPanel面板更宽，即使不能适合按钮表面的所有文本也同样如此。
* **水平对齐方式。**因为默认情况下按钮的HorizontalAlignment属性值设置为Stretch，所有StackPanel将尝试放大按钮以占满StackPanel面板的整个宽度。

理解这个过程的关键在于，要认识到最小尺寸和最大尺寸设置了绝对界限。在这些界限内，StackPanel面板尝试反应按钮所期望的尺寸以及对齐方式的设置。

#### 2.5 Border控件

Border控件不是布局面板，而是非常便于使用的元素，经常与布局面板一起使用。所以，在继续介绍其他布局面板之前，我们先介绍一下Border控件是有意义的。

Border类非常简单。它只能包含一段嵌套内容，并为其添加背景或在其周围添加边框。为了深入地理解Border控件，只需要掌握下表中列出的属性就可以了。

名称 | 说明
:---:|:---
Background | 使用Brush对象设置边框中所有内容后面的背景。可使用固定颜色背景，也可使用其他更特殊的背景
BorderBrush和BorderThickness | 使用Brush对象设置位于Border对象边缘的边框的颜色，并设置边框的宽度。为显示边框，必须设置这两个属性
CornerRadius | 该属性可使边框具有雅致的圆角。CornerRadius的值越大，圆角效果就越明显
Padding | 该属性在边框和内部的内容之间添加空间（与此相对，Margin属性在边框之外添加空间）

下面是一个具有轻微圆角效果的简单边框，该边框位于一组按钮的周围，这组按钮包含在一个StackPanel面板中：

```xml
<Border Margin="5" Padding="5" Background="LightYellow" 
BorderBrush="SteelBlue" BorderThickness="3,5,3,5" CornerRadius="3" VerticalAlignment="Top">
  <StackPanel>
	<Button Margin="3">One</Button>
	<Button Margin="3">Two</Button>
	<Button Margin="3">Three</Button>
  </StackPanel>
</Border>
```

## 三、WrapPanel和DockPanel面板 ##

显然，只使用StackPanel面板还不能帮助您创建出实用的用户界面。要设计出最终使用的用户界面，StackPanel面板还需要与其他更强大的布局容器协作。只有这样才能组装成完整的窗口。

最复杂的布局容器是Grid面板。在介绍Grid之前，有必要首先看一下WrapPanel和DockPanel面板，它们是WPF提供的两个更简单的布局容器。这两个布局容器通过不同的布局行为对StackPanel面板进行补充。

#### 3.1 WrapPanel面板

WrapPanel面板在可能的空间中，以一次一行或一列的方式布置控件。默认情况下，WrapPanel.Orientation属性设置为Horizontal；控件从左向右进行排列，再在下一行中排列。但可将WrapPanel.Orientation属性设置为Vertical，从而在多个列中放置元素。

提示：与StackPanel面板类似，WrapPanel面板实际上主要用来控制用户界面中一小部分的布局细节，并非用于控制整个窗口布局。例如可能使用WrapPanel面板以类似工具控件的方式将所有按钮保持在一起。

下面的示例中定义了一系列具有不同对齐方式的按钮，并将这些按钮放到一个WrapPanel面板中：

```xml
<WrapPanel Margin="3">
  <Button VerticalAlignment="Top">Top Button</Button>
  <Button MinHeight="60">Tall Button 2</Button>
  <Button VerticalAlignment="Bottom">Bottom Buttton</Button>
  <Button>Stretch Button</Button>
  <Button VerticalAlignment="Center">Centered Button</Button>
</WrapPanel>
```

正如这个示例，WrapPanel面板水平地创建了一系列假想的行，每一行的高度都被设置为所包含元素中最高元素的高度。其他控件能被拉伸以适应这一高度，或根据VerticalAlignment属性的设置进行对齐。因此，在该行中不关心各个按钮的VerticalAlignment属性的设置。

注意：WrapPanel面板是唯一一个不能通过灵活使用Grid面板代替的面板。

#### 3.2 DockPanel面板

DockPanel面板是更有趣的布局选项，它沿着一条外边缘来拉伸所包含的控件。理解该面板最简便的方法是，考虑一下位于许多Windows应用程序窗口顶部的工具栏。这些工具栏停靠到窗口顶部。与StackPanel面板类似，被停靠的元素选择它们布局的一个方面。

这里很明显的问题是：子元素如何选择停靠的边？

答案是通过Dock附加属性，可将该属性设置为Left、Right、Top或Bottom。放在DockPanel面板中的每个元素都会自动捕获该属性。

下面的示例在DockPanel面板的每条边上都停靠一个按钮：

```xml
<DockPanel LastChildFill="True">
  <Button DockPanel.Dock="Top">Top Button</Button>
  <Button DockPanel.Dock="Bottom">Bottom Button</Button>
  <Button DockPanel.Dock="Left">Left Button</Button>
  <Button DockPanel.Dock="Right">Right Button</Button>
  <Button>Remaining Space</Button>
</DockPanel>
```

该示例将DockPanel面板中的LastChildFill设置为true，该设置告诉DockPanel面板使用最后一个元素占满剩余空间。

显然，当停靠控件时，停靠顺序很重要。在这个示例中，顶部和底部按钮充满了DockPanel的整个边缘，这是因为这两个按钮首先被停靠。接着停靠左右按钮时，这两个按钮将位于顶部和底部之间。

可将多个元素停靠到同一边缘。这种情况下，元素按标记中声明的顺序停靠到边缘。而且，如果不喜欢空间分割或拉伸行为，可修改Margin属性、HorizontalAlignment属性以及VerticalAlignment属性，就像使用StackPanel面板进行布局时所介绍的那样。

#### 3.3 嵌套布局容器

很少单独使用StackPanel、WrapPanel、DockPanel面板。相反，它们通常用来设置一部分用户界面的布局。例如可使用DockPanel面板在窗口的合适区域放置不同的StackPanel和WrapPanel面板容器。

例如，假如希望创建一个标准对话框，在其右下角具有OK按钮和Cancel按钮，并且在窗口的剩余部分是一块较大的内容区域。在WPF中可采用几种方法完成这一布局，但最简单的方法如下，该方法使用前面介绍过的各种面板：

1）创建水平StackPanel面板，用于将OK按钮和Cancel按钮放在一起。
2）在DockPanel面板中放置StackPanel面板，将其停靠到窗口底部。
3）将DockPanel.ListChildFill属性设置为true，以使用窗口剩余的部分填充其他内容。在此可以添加另一个布局控件，或者只添加一个普通的TextBox控件。
4）设置边距属性，提供一定的空白空间。

下面是最终标记：

```xml
<DockPanel LastChildFill="True">
  <StackPanel DockPanel.Dock="Bottom" HorizontalAlignment="Right"
   Orientation="Horizontal">
	<Button Margin="10,10,2,10" Padding="3">OK</Button>
	<Button Margin="2,10,10,10" Padding="3">Cancel</Button>
  </StackPanel>
  <TextBox DockPanel.Dock="Top" Margin="10">This is a test.</TextBox>
</DockPanel>
```

在这个示例中，Padding属性在按钮边框与内部的内容之间添加了尽量少的空间。

乍一看，相对于使用坐标精确放置控件而言，这有些多余。在很多情况下确实如此。不过，设置时间固然较长，但这样做的好处是在将来可以很方便地修改用户界面。

与Windows旧式的用户界面框架相比，这里使用的标记更整洁、更简单也更紧凑。如果为这个窗口添加一些样式，还可对该窗口进行进一步改进，并移除不必要的细节，从而创建真正的自适应用户界面。

## 四、Grid面板 ##

Grid面板是WPF中功能最强大的布局容器。很多使用其他布局控件能完成的功能，用Grid面板也能实现。Grid面板也是将窗口分割成更小区域的理想工具。

实际上，由于Grid面板十分有用，因此在VS中为窗口添加新的XAML文档时，会自动添加Grid标签作为顶级容器，并嵌套在Window根元素中。

Grid面板将元素分隔到不可见的行列网格中。尽管可在一个单元格中放置多个元素，但在每个单元格中只放置一个元素通常更合理。当然，在Grid单元格中的元素本身也可能是另一个容器，该容器组织它所包含的一组控件。

提示：方便调试，我们可将不可见的Grid面板，通过将Grid.ShowGridLines属性设置为true，从而更清晰地观察Grid面板。这一特性并不是真正试图美化窗口，反而是为了方便调试，设计该特性旨在帮助理解Grid面板如何将其自身分割成多个小区域。

需要两个步骤来创建基于Grid面板的布局。首先，选择希望使用的行和列的数量。然后，为每个包含的元素指定恰当的行和列，从而在合适的位置放置元素。

Grid面板通过使用对象填充Grid.ColumnDefinitions和Grid.RowDefinitions集合来创建网格和行。例如如果确定需要两行和三行，可添加以下标签：

```xml
<Grid ShowGridLines="True">
  <Grid.RowDefinitions>
	<RowDefinition></RowDefinition>
	<RowDefinition></RowDefinition>
  </Grid.RowDefinitions>
  <Grid.ColumnDefinitions>
	<ColumnDefinition></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
	<ColumnDefinition></ColumnDefinition>
  </Grid.ColumnDefinitions>

  ...
</Grid>
```

正如本例演示的，在RowDefinition和ColumnDefinition元素中不必提供任何信息。如果保持他们为空，Grid面板将在所有行和列之间平均分配空间。在本例中，每个单元格的尺寸完全相同，具体取决于包含窗口的尺寸。

为在单元格中放置各个元素，需要使用Row和Column附加属性。这两个属性的值都是从0开始的索引数。例如以下标记演示了如何创建Grid面板，并使用按钮填充Grid面板的部分单元格。

```xml
<Grid ShowGridLines="True">
...
  <Button Grid.Row="0" Grid.Column="0">Top Left</Button>
  <Button Grid.Row="0" Grid.Column="1">Middle Left</Button>
  <Button Grid.Row="1" Grid.Column="2">Bottom Right</Button>
  <Button Grid.Row="1" Grid.Column="1">Bottom Middle</Button>
</Grid>
```

每个元素必须被明确地放在对应的单元格中。可在单元格中放置多个元素，或让单元格保持为空。也可以不按顺序声明元素，正如本例中的最后两个按钮那样。但如果逐行定义控件，可使标记更清晰。

此处存在例外情况。如果不指定Grid.Row属性，Grid面板会假定该属性的值为0。对于Grid.Column属性也是如此。因此，在Grid面板的第一个单元格中放置元素时可不指定这两个属性。

#### 4.1 调整行和列

如果Grid面板只是按照比例分配尺寸的行和列的集合，它也就没什么用处了。幸运的是，情况并非如此。为了充分发挥Grid面板的潜能，可更改每一行和每一列的尺寸设置方式。

Grid面板支持以下三种设置尺寸的方式：

* 绝对设置尺寸方式。设置设备无关单位准确地设置尺寸。这是最无用的策略，因为这种策略不够灵活，难以适应内容大小和容器大小的改变，而且更难以处理本地化。
* 自动设置尺寸方式。每行和每列的尺寸刚好满足需要。这是最有用的尺寸设置方式。
* 按比例设置尺寸方式。按比例将空间分割到一组行和列中。这是对所有行和列的标准设置。

为了获得最大的灵活性，可混合使用这三种尺寸设置方式。例如创建几个自动设置尺寸的行，然后通过按比例设置尺寸的方式让最后一行或两行充满剩余的空间，这通常是很有用的。

可通过将ColumnDefinition对象的Width属性或RowDefinition对象的Height属性设置为数值来确定尺寸的设置方式。例如下面代码显示了如何设置100设备无关单位的绝对宽度：

```xml
<ColumnDefinition Width="100"></ColumnDefinition>
```

为使用自动尺寸设置方式，可使用Auto值：

```xml
<ColumnDefinition Width="Auto"></ColumnDefinition>
```

为了使用按比例尺寸设置方式，需要使用星号（*）：

```xml
<ColumnDefinition Width="*"></ColumnDefinition>
```

如果混合使用按比例尺寸设置方式和其他尺寸设置方式，就可以在剩余的任意空间按比例改变行或列的尺寸。

如果希望不均匀的分割剩余空间，可指定权重，权重必须放在星号之前。例如如果有两行是按比例设置尺寸，并希望第一行的高度是第二行高度的一半，那么可以使用如下设置来分配剩余空间：

```xml
<RowDefinition Height="*"></RowDefinition>
<RowDefinition Height="2*"></RowDefinition>
```

上面的代码告诉Grid面板，第二行的高度应是第一行高度的二倍。可使用您喜欢的任何数字来分配剩下的空间。

使用这些尺寸设置方式，我们可以重现前面的示例对话框。使用顶级的Grid容器而不是使用DockPanel面板。下面是所需要的标记：

```xml
<Grid ShowGridLines="True">
  <Grid.RowDefinition>
	<RowDefinition Height="*"></RowDefinition>
	<RowDefinition Height="Auto"></RowDefinition>
  </Grid.RowDefinition>
  <TextBox Margin="10" Grid.Row="0">This is a test.</TextBox>
  <StackPanel Grid.Row="1" HorizontalAlignment="Right" Orientation="Horizontal">
	<Button Margin="10,10,2,10" Padding="3">OK</Button>
	<Button Margin="2,10,10,10" Padding="3">Cancel</Button>	
  </StackPanel>
</Grid>
```

#### 4.2 布局舍入

WPF使用分辨率无关的测量系统。尽管该测量系统为使用各种不同的硬件提供了灵活性，但有时也会引入一些问题。其中一个问题是元素可能被对齐到子像素边界——换句话说，使用没有和物理像素准确对齐的小数坐标定位元素。可通过为相邻的布局容器提供非整数尺寸强制发生这个问题。但当不希望发生这个问题时，在某些情况下该问题也可能出现，例如当创建按比例设置尺寸的Grid面板时就可能会发生该问题。

WPF在处理边界像素不为整数时，对微小的错位会使用反锯齿功能混合原本清晰的像素边界边缘。由于没有使用布局舍入（layout rounding），所以矩形的清晰边缘在特定的窗口尺寸下变得模糊了。

如果这个问题影响到布局，可以采用一种方法很方便地解决该问题。只需要将布局容器的UseLayoutRounding属性设置为true：

```xml
<Grid UseLayoutRounding="True">
```

现在，WPF会确保布局容器中的所有内容对齐到最近的像素边界，从而消除了所有模糊问题。

#### 4.3 跨越行和列

#### 4.4 分割窗口

#### 4.5 共享尺寸组

#### 4.6 UniformGrid面板

## 五、使用Canvas面板进行基于坐标的布局 ##

#### 5.1 Z顺序

#### 5.2 linkCanvas元素

## 六、布局实例 ##
