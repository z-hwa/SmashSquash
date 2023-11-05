2023.10.30  
	finish camera tracing(Unit)  
	link battle system -> camera system and player control system  
	finish unit instantiated in battle system(also init of battle env.)  
2023.11.3  
	debug: rebound magnitude fixed to right situation  
	setting mass and drag of rigid2D of unit  
2023.11.4  
	deal with unit data class and loading unit data in pre-combat phase  
2023.11.5  
	optimize camera tracing  
		Add recorder to tracing target  
		and let camera tracing the recorder  
		Add tracing error in traced logic to smooth tracing effct  
	Impact bounce effect between units are fixed  
		when a unit A smash anotheer unit B, A will be rebounded, B will be hit back  
	Combat round logic completed  
		start->player->enemy->...  
	Enemy default agent is completed  
		which wrote in unit templete A000  
