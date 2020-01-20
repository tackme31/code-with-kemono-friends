using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeWithKemonoFriends.Model
{
    public static class Friends
    {
        /// <summary>
        /// 乱数生成器
        /// </summary>
        private static readonly Random _rnd = new Random();

        /// <summary>
        /// ビルド成功時のメッセージ一覧
        /// </summary>
        public static readonly List<NotificationMessage> PassedMessages = new List<NotificationMessage>
        {
            new NotificationMessage { Speaker = "ジャガー", IconFileName = "ジャガー.png", Message = "すごいねー、魔法みたい！" },

            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "あなたはプログラミングが得意なフレンズなんだね！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "すっごーい！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "つくったー！？" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "すごいすごーい！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "それなに！？楽しそー！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "とまれー！うごけー！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "なにそれなにそれー！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "わたし、あなたの強い所、だんだんわかってきたよ！" },

            new NotificationMessage { Speaker = "かばん", IconFileName = "かばん.png", Message = "わぁ～、器用ですねー！" },

            new NotificationMessage { Speaker = "スナネコ", IconFileName = "スナネコ.png", Message = "すごいですね" },
            new NotificationMessage { Speaker = "スナネコ", IconFileName = "スナネコ.png", Message = "満足…" },

            new NotificationMessage { Speaker = "フェネック", IconFileName = "フェネック.png", Message = "おお～！よかったねー" },

            new NotificationMessage { Speaker = "コノハ博士", IconFileName = "コノハ博士.png", Message = "ちょいちょいです" },
            new NotificationMessage { Speaker = "コノハ博士", IconFileName = "コノハ博士.png", Message = "ここまで辿り着いた子は初めてです、これは期待できるです…" },
            new NotificationMessage { Speaker = "コノハ博士", IconFileName = "コノハ博士.png", Message = "いいとこまできてるですね" },

            new NotificationMessage { Speaker = "ミミちゃん助手", IconFileName = "ミミちゃん助手.png", Message = "朝飯前なのです" },
            new NotificationMessage { Speaker = "ミミちゃん助手", IconFileName = "ミミちゃん助手.png", Message = "やるですね！ようやく役に立ったのです" },

            new NotificationMessage { Speaker = "ヒグマ", IconFileName = "ヒグマ.png", Message = "最強すぎるだろ…" },

            new NotificationMessage { Speaker = "タイリクオオカミ", IconFileName = "タイリクオオカミ.png", Message = "お！いい顔頂き！" },

            new NotificationMessage { Speaker = "コツメカワウソ", IconFileName = "コツメカワウソ.png", Message = "たーのしー！" },

            new NotificationMessage { Speaker = "ハシビロコウ", IconFileName = "ハシビロコウ.png", Message = "じーーーーーー" },

            new NotificationMessage { Speaker = "アメリカビーバー", IconFileName = "アメリカビーバー.png", Message = "良かったっすね～、これで安心っす～！" },

            new NotificationMessage { Speaker = "アルパカ・スリ", IconFileName = "アルパカ・スリ.png", Message = "すご～い！" },

            new NotificationMessage { Speaker = "キンシコウ", IconFileName = "キンシコウ.png", Message = "お疲れ様ですー！" },

            new NotificationMessage { Speaker = "ジェーン", IconFileName = "ジェーン.png", Message = "やっとここまで来ましたね！" },
        };

        /// <summary>
        /// ビルド失敗時のメッセージ一覧
        /// </summary>
        public static readonly List<NotificationMessage> FailedMessages = new List<NotificationMessage>
        {
            new NotificationMessage { Speaker = "ジャガー", IconFileName = "ジャガー.png", Message = "いや、わからん…" },
            new NotificationMessage { Speaker = "ジャガー", IconFileName = "ジャガー.png", Message = "どうなるかぜんぜんわからん！" },

            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "あ、だめ！それはセルリアンだよ、逃げて！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "ここでちょっと休憩ー！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "わたしのせいじゃないよぉ！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "大丈夫！夜行性だから！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "こんなにがんばって作ったのに！いいじゃない！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "へーきへーき！フレンズによって得意な事違うから！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "わかんないや" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "とまれー！うごけー！" },
            new NotificationMessage { Speaker = "サーバル", IconFileName = "サーバル.png", Message = "うみゃー！負けないぞー！もういっかい！" },

            new NotificationMessage { Speaker = "かばん", IconFileName = "かばん.png", Message = "どうしてこんなことに…" },

            new NotificationMessage { Speaker = "ボス", IconFileName = "ボス.png", Message = "アワワワワワワワワワワワ" },
            new NotificationMessage { Speaker = "ボス", IconFileName = "ボス.png", Message = "マカセテ" },

            new NotificationMessage { Speaker = "スナネコ", IconFileName = "スナネコ.png", Message = "今日はここまでにしておくです" },
            new NotificationMessage { Speaker = "スナネコ", IconFileName = "スナネコ.png", Message = "……でもまあ、騒ぐほどでもないか" },
            new NotificationMessage { Speaker = "スナネコ", IconFileName = "スナネコ.png", Message = "噂通りのドジっ子ですね！" },

            new NotificationMessage { Speaker = "フェネック", IconFileName = "フェネック.png", Message = "まーまー、気軽に行こーよ－" },
            new NotificationMessage { Speaker = "フェネック", IconFileName = "フェネック.png", Message = "またやってしまったね～" },
            new NotificationMessage { Speaker = "フェネック", IconFileName = "フェネック.png", Message = "急いじゃだめだってばー…" },

            new NotificationMessage { Speaker = "コノハ博士", IconFileName = "コノハ博士.png", Message = "さあ、とっとと野性解放するのです！" },
            new NotificationMessage { Speaker = "コノハ博士", IconFileName = "コノハ博士.png", Message = "これはだめですね…" },

            new NotificationMessage { Speaker = "ミミちゃん助手", IconFileName = "ミミちゃん助手.png", Message = "われわれが満足できるまで頑張るのです！" },
            new NotificationMessage { Speaker = "ミミちゃん助手", IconFileName = "ミミちゃん助手.png", Message = "だめなのです…" },
            new NotificationMessage { Speaker = "ミミちゃん助手", IconFileName = "ミミちゃん助手.png", Message = "ポンコツだらけなのです、まったく…" },

            new NotificationMessage { Speaker = "ヒグマ", IconFileName = "ヒグマ.png", Message = "残念だが、切り替えろ…" },

            new NotificationMessage { Speaker = "アライグマ", IconFileName = "アライグマ.png", Message = "パークの危機なのだー！" },
            new NotificationMessage { Speaker = "アライグマ", IconFileName = "アライグマ.png", Message = "ダメなのだー！" },

            new NotificationMessage { Speaker = "カバ", IconFileName = "カバ.png", Message = "本当につらい時は、誰かを頼ったっていいのよ？" },
            new NotificationMessage { Speaker = "カバ", IconFileName = "カバ.png", Message = "あなた、何にもできないのねぇ" },

            new NotificationMessage { Speaker = "コツメカワウソ", IconFileName = "コツメカワウソ.png", Message = "え！死んじゃった？" },

            new NotificationMessage { Speaker = "アルパカ・スリ", IconFileName = "アルパカ・スリ.png", Message = "お客さんじゃないのかぁ…ペッ！" },

            new NotificationMessage { Speaker = "ツチノコ", IconFileName = "ツチノコ.png", Message = "落ち着くんだよッ！" },
            new NotificationMessage { Speaker = "ツチノコ", IconFileName = "ツチノコ.png", Message = "お前の鼻は飾りかぁッ！" },

            new NotificationMessage { Speaker = "キンシコウ", IconFileName = "キンシコウ.png", Message = "大丈夫ですか？" },

            new NotificationMessage { Speaker = "ライオン", IconFileName = "ライオン.png", Message = "気楽にやりゃあーいいよ～！" },

            new NotificationMessage { Speaker = "ヘラジカ", IconFileName = "ヘラジカ.png", Message = "私たちならやれる！" },

            new NotificationMessage { Speaker = "アメリカビーバー", IconFileName = "アメリカビーバー.png", Message = "ど、どうなるっすか…？どうなっちゃうんすか～！" },

            new NotificationMessage { Speaker = "リカオン", IconFileName = "リカオン.png", Message = "オーダーキツイですよー" },

            new NotificationMessage { Speaker = "アミメキリン", IconFileName = "アミメキリン.png", Message = "これは怪事件だわ…" },
        };

        /// <summary>
        /// ランダムにビルド成功時メッセージを取得します
        /// </summary>
        public static NotificationMessage GetRandomPassedMessage() => PassedMessages[_rnd.Next(PassedMessages.Count)];

        /// <summary>
        /// ランダムにビルド失敗時メッセージを取得します
        /// </summary>
        public static NotificationMessage GetRandomFailedMessage() => FailedMessages[_rnd.Next(FailedMessages.Count)];
    }
}
