# AddressBook
ソリューション一式をクローンしたらビルドは通るはず。
ローカルで動作させるための儀式
・SQL Server(Express/Developper)のインストール
　http://afmodeler.com/programming/intro_sqlserver/
・データベースの作成
　デフォルトはlearn
・テーブルの作成
　sql\ddl.sqlを実行する。
　http://afmodeler.com/uncategorized/design_db/
・接続文字列の修正
　AddressBook\AddressBook\App.configを修正する。

儀式が終了したらビルドしたアプリを実行してマスタ管理画面で郵便番号データおよび電話番号データを更新する。

