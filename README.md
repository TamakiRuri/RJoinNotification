# RJoinNotification

VRChatワールド用の入退室通知システムです。

このギミックを利用するには[Canvas Animation System](https://github.com/puk06/CanvasAnimationSystem)を予め導入する必要があります。

導入リンク: https://vpm.pukosrv.net/addRepo

このギミックを導入するには、VPMをご利用ください。

[導入リンク](vcc://vpm/addRepo?url=https://vpm.rurinya.com/vpm.json)

または、VCC・ALCOMでレポジトリ https://vpm.rurinya.com/vpm.json を追加してください。

その後、Packages/RJoinNotification からプレハブをヒエラルキーの任意な位置に入れてください。（親オブジェクトが有効になっていれば順番に影響がありません）

---

環境:

Unity 2022.3.22f1

VRChat SDK 3.10.1以降

## プレハブ

Packages/RJoinNotificationにあるプレハブの違いは、主に画面上通知の場所と、デフォルトのモードにあります。

#### RJoinNotification

通知が画面の下部の中央に設置されています。デフォルトモード: Fade

#### RJoinNotification_Left

通知が画面の下部の左側に、左に20度回転された状態で設置されます。デフォルトモード：Fade In From Left

#### RJoinNotification_Right

通知が画面の下部の右側に、右に20度回転された状態で設置されます。デフォルトモード：Fade In From Right

## Settings / 設定

### Join Text / Exit Text

入室・退室時に表示される通知の内容です。

### Join / Exit Info Color

入室・退室の通知の色です。

### Transition In Time / Transition Out Time

アニメーションが出現、消失するアニメーションの長さです。

### Stay Time

出現アニメーションが終わってから、消失アニメーションが始まるまでの長さです。

### Has Background

不透明なバックグラウンドが追加されます。

### Audio Only

音声のみ流れます。

### Mode モード

- Fade In (フェード)
- Pop (ポップ)
- Fade In From Left (左からフェード)
- Fade In From Right (右からフェード)
- Fade In From Below (下からフェード)

Popがほかのモードと比べて2倍程度重くなっております。

軽量化のため、下のAllow Multiple Notificationsは、必ずオフにしてください。

### Pop Mode Offset

Pop Modeを利用する場合で、右へのオフセットです。デフォルト:120。

### Allow Multiple Notifications

同時に複数人が入退室した場合では、複数（Notificationオブジェクトの数分）の通知が重ねて表示されます。

バックグラウンドがない場合では、文字が重ねて表示されるため、読みにくくなる場合があります。

### Join Sound / Exit Sound

入室・退室音です。

### Notifications

表示される通知のオブジェクトです。多いほどAllow Multiple Notificationsで表示できる通知の数が多くなりますが、重くなります。順番に影響がないが、同じオブジェクトを複数スロットに入れないでください。

### Canvas Aninmation System (Max Concurrent Animations)

RJoinNotificationにあるCanvasオブジェクトにアタッチされています。

同時実行できるアニメーションの数です。

Pop以外のおすすめ: 64 Popのおすすめ: 128

Pop以外はNotificationオブジェクトの数\*12+10, PopではNotificationオブジェクトの数\*22+10にしてください。

ほかの設定は確認用のため、変更しても効果がありません。

### R Join Notification Object

Notification Objectの設定は、すべてレファレンスになっております。

そのため、通常使用では編集する必要がありません。


### isMuted オブジェクト

RJoinNotificationの下にあるisMutedオブジェクトは、入退室通知の音が流れるかどうかを制御するオブジェクトです。このオブジェクトは外部のスイッチなどによる操作が可能です。

例えば、Lura's SwitchのTargetObjectに入れると、Lura's Switchでの制御が可能です。

## ライセンス

MIT

詳しくは[ライセンス](./LICENSE)にご参照ください。

## もし問題がありましたら

Issuesを投稿していただけると助かります。