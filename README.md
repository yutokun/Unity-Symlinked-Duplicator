# Unity Symlinked Duplicator

English follows Japanese

1つのプロジェクトで Unity を複数起動するための、シンボリック・リンクされたディレクトリを作成します。コマンド打つのがめんどくさすぎて作りました。

https://user-images.githubusercontent.com/8219697/143734270-90bd2166-65a4-4102-a05f-722c5ae68a52.mp4

## 動作環境

- Windows 10
- [.NET Destop Runtime 6.0.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## 使い方

Unity プロジェクトのフォルダを本アプリにドロップすると、`フォルダ名 Symlinked` というフォルダを作成し、Assets、Project Settings、Packages、Library のシンボリック・リンクを作成します。

起動する Unity 間でプラットフォームが異なる場合は、Library を分けた方が無難かもしれません。

## ライセンス

[The Unlicense](LICENSE)

---

# Unity Symlinked Duplicator

Create a symbolically linked directory for multiple Unity launches of a single project. I made it because it was too much of a hassle to type commands.

https://user-images.githubusercontent.com/8219697/143734270-90bd2166-65a4-4102-a05f-722c5ae68a52.mp4

## Prerequisites

- Windows 10
- [.NET Destop Runtime 6.0.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## Usage

If you drop a folder of your Unity project into this app, it will create a folder named `Folder name Symlinked` and create symbolic links for Assets, Project Settings, Packages, and Library.

If you are running Unity(s) on different platforms, it may be safer to exclude the Libraries.

## License

[The Unlicense](LICENSE)
