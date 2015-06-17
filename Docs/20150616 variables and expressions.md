此文档主要记录下C#入门经典第三章：变量与表达式这一章的内容笔记。

#####C#的基本语法

1. 代码段一般包裹在定界符花括号之内{}。
2. 注释风格1：注释开头结尾标记，可以使单行注释，也可以是多行注释。/**/
3. 注释风格2：使用//开始一个注释。只用于单行注释。
4. 注释风格2的扩展：使用三个/，这类注释可以通过IDE的配置，被自动提取生成文档说明书。
5. C#代码区分大小写。

        /*这是第一种注释风格 */
        /*也可以进行多行注释，第一行
          第二行
          ......
          第N行*/
        //这是第二种注释风格
        ///这是特殊的注释风格

在C#中，遇到以#开头的代码一般是预处理指令，这里介绍两个，#region #endregion，这两个关键字可以定义可展开和折叠的代码区域的开头和结尾。


#####变量及用法

声明变量的C#语法是指定类型和变量名：`<type> <name>;`

基本类型：

整数类型

<link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.5/css/bootstrap.min.css">
<table class="table table-bordered table-striped table-condensed">
   <tr>
      <td>&#31867;&#22411;</td>
      <td>&#21035;&#21517;</td>
      <td>&#20540;</td>
   </tr>
   <tr>
      <td>sbyte</td>
      <td>System.Sbyte</td>
      <td>&#22312;-128~127&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>byte</td>
      <td>System.Byte</td>
      <td>&#22312;0~255&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>short</td>
      <td>System.Int16</td>
      <td>&#22312;-32768-32767&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>ushort</td>
      <td>System.UInt16</td>
      <td>&#22312;0-65535&#38388;&#25972;&#25968;</td>
   </tr>
   <tr>
      <td>int</td>
      <td>System.Int32</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>uint</td>
      <td>System.UInt32</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>long</td>
      <td>System.Int64</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td>ulong</td>
      <td>System.UInt64</td>
      <td>&#30053;</td>
   </tr>
   <tr>
      <td></td>
   </tr>
</table>

浮点型数字类型：

<table class="table table-bordered table-striped table-condensed">
   <tr>
      <td>&#31867;&#22411;</td>
      <td>&#21035;&#21517;</td>
      <td>&#20540;</td>
   </tr>
   <tr>
      <td>float</td>
      <td>System.Single</td>
      <td>&#32422;1.5*10^-45~3.4*10^38</td>
   </tr>
   <tr>
      <td>double</td>
      <td>System.Double</td>
      <td>&#32422;5*10^-324~1.7*10^308</td>
   </tr>
   <tr>
      <td>decimal</td>
      <td>System.Decimal</td>
      <td>&#32422;1*10^-28~7.9*10^28</td>
   </tr>
   <tr>
      <td></td>
   </tr>
</table>

其他数据类型：

| 类型	 | 别名	          | 值                             |
|:------:|:--------------:|:----------------------------- :|
| char	 | System.Char    | 一个Unicode字符，0—65535间的整数 |
| bool   | System.Boolean | 布尔值，true或false             |
| string | System.String  | 字符串                          |
