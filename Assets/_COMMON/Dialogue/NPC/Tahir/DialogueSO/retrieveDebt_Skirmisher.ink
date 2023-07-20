#speaker:2:	Tahir	
Me?
		
#speaker:1:	Abbas	
Yes, you Tahir, or whoever the pit you are!
		
#speaker:2:	Tahir	
Look if you are here for trouble, I’m not the right man to brawl with.
		
#speaker:1:	Abbas	
I know, you are just a petty thief, taking an old man’s money and not paying it back. I’m here to collect it.
		
#speaker:2:	Tahir	
What? What in the eight seas you are talking about? Have you lost your mind?
		
#speaker:1:	Abbas	
No, but my uncle has. His name is Khalid and you know him very well and that he lost his memory. Tell me you greedy bastard, are you paying your debt or should I take it by force?
		
#speaker:2:	Tahir	
Calm down, calm down... I see you’re trying to protect the old man, but he’s already crazy as a loon. He never remembers anything. I swear i have no pending debt. I paid it back already years ago.
		
#speaker:1:	Abbas	
Do you take me for a fool? I won’t fall for such an easy ruse! (a harsh stare by Abbas startles the man)
		
#speaker:2:	Tahir	
No of course not! I mean i don’t take you a fool and i’m not a fool either, so listen(he whispers to your ear) I have an offer for you!
		
#speaker:1:	Abbas	
Go on!
		
#speaker:2:	Tahir	
I will basically give you a small portion of this.. so-called debt that he is talking about, you know… You look like a decent boy, you can just have a couple of drinks and so on…
		
#speaker:1:	Abbas	
In return?
		
#speaker:2:	Tahir	
In return, you will just go and tell the old man the truth.
		
#speaker:1:	Abbas	
The truth?
		
#speaker:2:	Tahir	
Yes, the truth that I don’t owe him anything.

#speaker:1:	Abbas

->dialogchoice
->END
=== dialogchoice ===

		# interaction: 1:dialog choice
		# define :1:  Gain positive Fame yield
		# define :2:  Gain negative Fame yield
     
		*[Accept his honest offer]
		Hmm, well… I see no wrong in telling the truth to a troubled old man. I will take your deal and I will take a few silver coins from you just as a compensation.

		    ->OnDialogChoiceCompleted
		    
        *[Teach him a lesson]
Abbas grabs two apples from the Tavern stall and start juggling them receiving the attention of the people including the man. Then suddenly he swings the hatchet and cuts the apples in half with a clean strike.

He grabs the top halves with the empty hand while kicking one of the other halves with a foot before it lands on floor. It hits the man right on his head. Abbas approaches Tahir again, while people are laughing.
            ->OnDialogChoiceCompleted

-> END
===OnDialogChoiceCompleted===

#speaker:1:	Abbas	
(calmly hands an apple half to the man and bites into the other half)
 You know what other things I can use that axe to cut in half? 
		
#speaker:2:	Tahir	
(bites the apple timidly)
I doubt I want to know.
		
#speaker:1:	Abbas	
(blinks with a little grin on his face.)
Can I get the money then?
		
#speaker:2:	Tahir	
How much?
		
#speaker:1:	Abbas	
As much as you owe.
		
#speaker:2:	Tahir	
(he hands over two silver coins)
Here it is then.
		
#speaker:1:	Abbas	
Wait let me ask my friend first! 
(Reaches for his rungu, flips it in his hand once)

		
#speaker:1:	Abbas	
Tell me boy, is it two silver coins? No? How much then? Three? Still no? Five? Oh ok then!
		
#speaker:2:	Tahir	
(he hands in the denari in a terrified manner) Here, take it! Four silver and a bronze sixer. I swear it is not more than this.
		
#speaker:1:	Abbas	
Take it easy, I believe you!
-> END







