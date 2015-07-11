这一章笔记我们主要整理一些OOP实现过程中的一些技巧，主要包括集合、字典、迭代器、类型比较、值比较等内容。本章的实例也比较多，更是要将我们创建的CardLib库进行多次优化。

# 集合、比较、转换 #

## 一、集合 ##

System.Collections命名空间中的几个接口提供了基本的集合功能：

* IEnumerable可以迭代集合中的项。
* ICollection（继承于IEnumerable）可以获取集合中的项的个数，并能把项复制到一个简单的数组类型中。
* IList（继承于IEnumerable和ICollection）提供了集合的项列表，允许访问这些项，并提供其他的一些与项列表相关的基本功能。
* IDictionary（继承于IEnumerable和ICollection）类似于IList，但提供了可通过键值（而不是索引）访问的项列表。

System.Array实现了IEnumerable、ICollection、IList，但不支持IList的一些更高级的功能，它表示大小固定的项列表。

#### 1、使用集合

#### 2、定义集合

#### 3、索引符

#### 4、关键字值集合和IDictionary

#### 5、迭代器

【迭代器和集合】

#### 6、深赋值

## 二、比较 ##

#### 1、类型比较

###### 1）封箱和拆箱

###### 2）is运算符

#### 2、值比较

###### 1）运算符重载

###### 2）IComparable和IComparer接口

###### 3）使用IComparable和IComparer接口对集合排序

## 三、转换 ##

#### 1、重载转换运算符

#### 2、as运算符




