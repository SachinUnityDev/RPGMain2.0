->main
=== main === 

#speaker:2:	Khalid	
Around... umm, I don’t remember.		
				
#speaker:1:	Abbas	
I would have been surprised if you did. Here, take it. 		
				
#speaker:2:	Khalid	
Are you serious? How? I mean, I don’t remember that well but the guy was a troublemaker. That’s the least i know.		
				
#speaker:2:	Khalid	
(smiles and places his hand on Abbas’ shoulder) You really are a boy with guts, aren’t you?
				
#speaker:1:	Abbas	
You tell me... Anyway, I am really tired. It has been a long day for me. If you allow me to stay here, as you promised, I would appreciate getting a good sleep.		
				
#speaker:2:	Khalid	
What promise you are talking about? I don’t remember.		
				
#speaker:1:	Abbas	
Are you f.cking serious?		
				
#speaker:2:	Khalid	
Joking, joking... No worries, you can stay. But…		
				
#speaker:1:	Abbas	
But what?		
				
#speaker:2:	Khalid	
I forgot what to say. Whatever… So you can use this floor bed and keep your belongings in this chest. Don’t touch my herbs, scrolls or potions. I will give you one or two for a start but that's it. If you want to purchase more, go to my sweet blondie.		
				
#speaker:1:	Abbas	
Who is that?		
				
#speaker:2:	Khalid	
(shrugs) Apothecary of course, who else? 
	
				
#speaker:2:	Khalid	
How old are you man? Why are you talking so much about this woman?		
				
#speaker:1:	Abbas	
My wife died four years ago. Do you know how it feels to be lonely for four years?		
				
#speaker:2:	Khalid	
It is just four, not forty! Get over yourself!		
				
#speaker:1:	Abbas	
My sweet wife… I miss her.		
				
#speaker:2:	Khalid	
What happened to her?		
				
#speaker:1:	Abbas	
Long story short she is dead. But what to do heh? Let’s not waste our breathe on the dead. Gone is gone. 		
				
#speaker:2:	Khalid	
I love how emotional you are.		
				
#speaker:1:	Abbas	
Yes, right? Anyway, let me tell you little bit about this town, open your ears and listen to me. 		
				
#speaker:2:	Khalid	
Listening…		
				
#speaker:1:	Abbas	
Nekkisari is bigger than your village but it is not as big as you think it is. The word gets around quickly here. So every move you make will work towards your Fame.		
				
#speaker:2:	Khalid	
It is up to you kid. I can’t change who you are or who you want to be, but you can be sure that everything you do will bring its own consequences. You have to choose your own path after all, whether you want to be a Hero or a Villain.		
				
#speaker:1:	Abbas	
Why should I be a Hero or a Villain? I am here just until you gather your mind.		
				
#speaker:2:	Khalid	
Fame is important kid. And my mind will not recover so quickly, that I know. Be prepared to stay here for a while. 		
				
#speaker:1:	Abbas	
I don’t like the sound of this…		
				
#speaker:2:	Khalid	
Meet the city folk first. Talk with Greybrow the Tavernkeeper. He is a tough guy but he is scared of rats. He might need your hand to clean up the sewers. Reach out to him. 

#speaker:1:	Abbas	
Well… I will see what I can do. Who else could help me out here? 		
				
#speaker:2:	Khalid	
The Keeper of the Temple of Shalulu is a very attractive woman called Minami. She came from Kushima… A long long way from here. She is a Soothsayer and a wielder of Water Magic. She can enchant your weapon with gems and clear your mind with her own techniques. 		
				
#speaker:1:	Abbas	
I make sure she feels home here. You should do same. Though i am not sure if she would accept you just like that. Maybe i will talk to her some day to let you in.		
				
#speaker:2:	Khalid	
Creating your own excuses to talk to her. I see.		
				
#speaker:1:	Abbas	
Oh, almost forgot the marketplace. There isn’t just an Apothecary there. Meet with the Fruiterer. Fruits are healthy and delicious. You can eat whatever I have here at home but you need to buy for yourself if you need extra.		
				
#speaker:2:	Khalid	
Fair enough. Is there a workshop?		
				
#speaker:1:	Abbas	
Yes. The owner Amadi, he is an honest guy. Don’t mess with him. He won’t do business with you if your Fame is Notorious or worse. 		
				
#speaker:2:	Khalid	
I will not make my decisions for the sake of doing business with one guy but thanks for the tip.		
				
#speaker:1:	Abbas	
I almost forgot. There is Amish. If you wanna buy flashy stuff, like necklaces, rings, gloves, belts… gewgaws overall, speak with him. He is a bit tricky though, watch out. 		
				
#speaker:2:	Khalid	
Anyone else? 		
				
#speaker:1:	Abbas	
The Safekeeper! Leon is a good man. And he is smart like a fox. He made a name for himself in short time with his Safekeeping services.  When you have enough coins to spend, go to him, he will help you with investments on city buildings.		
				
#speaker:2:	Khalid	
But he won’t receive you until you got some coins to spend. Then he can safekeep your denari and items too. You know, even this house might not be safe. Thieves are swarming around here. 		
				
#speaker:1:	Abbas	
I hope I will encounter some in action. I’d kick their asses!		
				
#speaker:2:	Khalid	
They are well organized though. They have a guild. I suspect the house in the back street is the headquarters. You can check what they are doing at night but watch your pockets.		
				
#speaker:1:	Abbas	
I will. Anyone else I should be careful of?		
				
#speaker:2:	Khalid	
For now kid, I would say, stay away from the City Hall. Try not to attract the guards’ attention. Maybe later you will need to deal with them, but not now. Oh, and there is a list of available Bounties hanging on the tavern wall. Just in case, if you need...		
				
#speaker:1:	Abbas	
I don't think I need, but thanks anyway.		
				
#speaker:2:	Khalid	 
Don’t see such side missions as a burden, they might help you gain a lot of loot, and experience. And come see me tomorrow morning. Let’s get you a job!		
				
#speaker:1:	Abbas	
I don’t like to work but I guess you are gonna kick me out if I don’t bring in any coins after the first week. 		
				
#speaker:2:	Khalid	
Be a man and work! You are not a kid just because I call you kid. Tell me, what are you capable of?		


->jobchoice
->END
=== jobchoice ===

		# interaction: 1:job choice
		
		# define :1:  unavailable for Demo
		# define :2: My people are woodcutters by trade. I think that’s what I can do best.

         
		*[Poacher]
		    ->OnJobChoiceCompleted
		    
        *[Woodcutter]
            I’m not really a warrior type, though I do have my hatchet to swing and my rungu to throw. I’ve been called as the Skirmisher in Adjholo.
            ->OnJobChoiceCompleted
 

-> END

=== OnJobChoiceCompleted ===

#speaker:2:	Khalid	
That’s fine. Tomorrow I will introduce you to your supervisor. He will tell you what to do. Come in during the day, not at night!
		
#speaker:1:	Abbas	
Okay!
		
#speaker:2:	Khalid	
Good night then kid, I will see you tomorrow, if you are still alive, hehe!
		
#speaker:1:	Abbas	
You are such a motivator!

->END
