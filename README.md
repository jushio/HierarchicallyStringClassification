# 概要

文字列の階層的分類を行う。分類方法は単純なものを実装している。区切り文字によって文字列を部分文字列に分解し、部分文字列をクラスラベルと見立てて、部分文字列によって指定されるクラスに文字列を分類する。具体例は[分類例](#section)に記述した。



# 引数

`HierarchicallyStringClassification.exe fileName [rowNum] [depth]`

1. fileName

入力データのファイル名。ファイル内の各行の文字列を1要素とする。ただし引数 rowNum を指定することにより、入力ファイルを空白区切りのテーブルと見立てて、指定列についてのみ分類させることができる。

2. rowNum

オプショナル。使わない場合は指定しないかマイナス値にする。0 以上の数を与えると、ファイルを空白区切りのテーブル形式のデータと見做し、番号が rowNum である列のみを分類するモードに変更する。

3. depth

オプショナル。使わない場合は指定しないかマイナス値にする。0 以上の数を与えると、出力について指定した深さ以上の階層は表示しない。**depth は rowNum を指定しない場合は指定できない。**この無意味な制限は将来的になくなる予定。


# 分類方法


## 具体的な手順

以下の手順による。

1. 文字列を特定の区切り文字で切った部分文字列に分解する。

2. 各部分文字列をクラスラベルと見立てる。左側（文字頭に対応する）の部分文字列が最上位クラスのラベル、右に行くにつれて低位クラスのラベルであるとする。このようにして該当するクラスラベルのクラスタに文字列を分類する。

区切り文字は`/`と`_`と`[`と`]`で固定。将来的には引数で指定する仕様に変更予定。


## <a name="section">分類例

次の 3 つの文字列の分類について示す。


s1: `core/ex_pc_regs[1]`


s2: `core/ex_pc_regs[2]`


s3: `core/ex_insts_regs[0]`

### s1 の分類
1. 分解

`core/ex_pc_regs[1]` => `"core", "ex", "pc", "regs", "1"`

2. 分類

```text:clustering of s1
[root]
-"core"
--"ex"
---"pc"
----"regs"
-----"1"
```

### s2 の分類
1. 分解

`core/ex_pc_regs[2]` => `"core", "ex", "pc", "regs", "2"`

2. 分類

```text:clustering of s1
[root]
-"core"
--"ex"
---"pc"
----"regs"
-----"1"
-----"2"
```

### s3 の分類

1. 分解

`core/ex_insts_regs[0]` => `"core", "ex", "insts", "regs", "0"`

2. 分類

```text:clustering of s1
[root]
-"core"
--"ex"
---"insts"
----"regs"
-----"0"
---"pc"
----"regs"
-----"1"
-----"2"
```


# 補足

**数値は表示する時に煩雑なので、同レベルのクラスラベルに数値が複数存在する時は、まとめて表示する。**

`1,2,3,5,7,8,9,10` => `[1-3,5,7-10]`
