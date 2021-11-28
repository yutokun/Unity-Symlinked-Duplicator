# Unity Symlinked Duplicator

English follows Japanese

1つのプロジェクトで Unity を複数起動するための、シンボリック・リンクされたディレクトリを作成します。コマンド打つのがめんどくさすぎて作りました。

https://user-images.githubusercontent.com/8219697/143734270-90bd2166-65a4-4102-a05f-722c5ae68a52.mp4

## 動作環境

- Windows 10
- [.NET Destop Runtime 6.0.0](https://dotnet.microsoft.com/download/dotnet/6.0)

## 使い方

1. Unity プロジェクトのフォルダを本アプリにドロップします。
2. シンボリック・リンク作成のための管理者権限を許可します。

`フォルダ名 Symlinked` というフォルダと、Assets、Project Settings、Packages、Library へのシンボリック・リンクが作成されます。

起動する Unity 間でプラットフォームが異なる場合は、Library を分けた方が安全かもしれません。

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

1. Drop folders of the Unity project into this application.
2. Allow administrator privileges for symbolic link creation.

The app will create a folder named `Folder Name Symlinked` and symbolic links to Assets, Project Settings, Packages, and Library.

If you will running Unity(s) on different platforms, it may be safer to exclude the Libraries.

## License

[The Unlicense](LICENSE)
