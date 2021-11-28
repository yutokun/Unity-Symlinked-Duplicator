# Unity Symlinked Duplicator

1つのプロジェクトで Unity を複数起動するための、シンボリック・リンクされたディレクトリを作成します。コマンド打つのがめんどくさすぎて作りました。

## 動作環境

- Windows 10
- [.NET Destop Runtime 6.0.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## 使い方

Unity プロジェクトのフォルダを本アプリにドロップすると、`フォルダ名 Symlinked` というフォルダを作成し、Assets、Project Settings、Packages、Library のシンボリック・リンクを作成します。

起動する Unity 間でプラットフォームが異なる場合は、Library を分けた方が無難かもしれません。