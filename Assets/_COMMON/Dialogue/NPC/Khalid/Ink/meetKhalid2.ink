->main
=== main === 

# speaker : 2:	Khalid	
		So, tell me kid, how may i help you?
# speaker : 1:	Abbas	
		Well... I am Abbas, son of…
# speaker : 2:	Khalid		
		No, this time no joke, sorry. I don’t know no Hakeem nor Kareem. Maybe you are mistaken?	
# speaker : 1:	Abbas		
		(sighs) Came all the way down for nothing. Look I don’t even have much left in my pocket to stay in the tavern for a night. And I walked down here from Adjholo. At least can i...

		

->classchoice
->END
=== classchoice ===

		# interaction: 1:hello
		
		# define :1:  unavailable for Demo
		# define :2: Hunter archetype. High dmg    , dodge but low armor.
         # define:3: unavailable for Demo
         
		*[Warden]
		    I was the Warden in my village. I don’t like bragging but people praised me for my physical prowess and polearm skills.
		    ->OnClassChoiceCompleted
        *[Skirmisher]
            I’m not really a warrior type, though I do have my hatchet to swing and my rungu to throw. I’ve been called as the Skirmisher in Adjholo.
            ->OnClassChoiceCompleted
        *[Herbalist]
            I have been studying the art of Herbalism in my village since childhood. Praise to earth mother Ruru, I am a man of nature. Trees are my friends, animals are my brothers.
            ->OnClassChoiceCompleted
                

-> END

=== OnClassChoiceCompleted ===

# speaker : 2:	Khalid		
			That’s good, that’s good. That means you are qualified to retrieve what i could not, from that bastard. If you do that, i would not request any money from you for a week of stay. After that, we will see. Works for you?
# speaker : 1:	Abbas		
			Well, do i have another choice?
# speaker : 2:	Khalid		
			Not if you wanna stay here.
# speaker : 1:	Abbas		
			Alright then, tell me where to find this guy and what his name is.
# speaker : 2:	Khalid		
			Forgot his name, but he can be easily identified by the precious amber necklace he wears. Bought from the beautiful Apothecary of Nekkisari. Ahh she is so sweet, so cute…
# speaker : 1:	Abbas		
			Clean your face, you’re drooling. We were talking about the man, not the lady by the way.
# speaker : 2:	Khalid		
			Ah yeah sorry… Anyways, he should be hanging around the tavern. You can check him out some time in the evening. Ask Greybrow the Tavernkeeper.
# speaker : 1:	Abbas		
			Alright, i will see it done. And after that, i will see your memory refreshed, hopefully.

		
->END
