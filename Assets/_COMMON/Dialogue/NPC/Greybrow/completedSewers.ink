#speaker:2:	Greybrow	
You are back! You really did it?	
			
#speaker:1:	Abbas	
Yes! Though squeaks of the mouse and rat are rattling in my head like a music band!	
			
#speaker:2:	Greybrow	
You are great man Abbas. I owe you.	
			
#speaker:1:	Abbas	
And sorry for messing with you about the rat looking like a lion king. He really did! Now he is just a skeleton king.	
			
#speaker:2:	Greybrow	
See? You didn’t believe me. Anyway thanks, it helped me a lot. Here i will give you some supplies to help you in the quests to come.	
			
#speaker:1:	Abbas	
Is that it? 	
			
#speaker:2:	Greybrow	
Don’t be greedy!	
			
#speaker:1:	Abbas	
Come on man, I put my life on the line for you!	
			
#speaker:2:	Greybrow	
Okay, fair enough. Well, I can’t pay you, but I can do something else for you. 	
			
#speaker:1:	Abbas	
What could be more valuable than denari, I wonder…	
			
#speaker:2:	Greybrow	
I can tell my customers all about you, to build up your fame! Or if you’d rather be the Villain of the city, I can speak of your misdeeds and see your fame decreased. Whichever you wish. 

You must choose though, I can’t do both. 
->dialogchoice
->END
=== dialogchoice ===

		# interaction: 1:dialog choice
		
		# define :1:  Gain positive Fame yield
		# define :2:  Gain negative Fame yield
     
		*[Be the Righteous Hero]
		Good, from now on, I will spread the word of your virtues. Every other day you will have your fame increase. People will eventually accept you as their hero!
		    ->OnDialogChoiceCompleted
		    
        *[Be the Villain the City deserves]
            Very well then, that’s your choice. I will tell everyone how despicable and mean you are, until you are the most notorious person in the city. Maybe you will be the villain this city needs!
            ->OnDialogChoiceCompleted

===OnDialogChoiceCompleted===
#speaker:1:	Abbas	
You know what Greybrow, I feel really luck to have a friend like you!
		
#speaker:2:	Greybrow	
Me too, browther, me too!
		
#speaker:1:	Abbas	
(a fake smile hangs from his cheek) That was funny, I give you that…

-> END
-> END






