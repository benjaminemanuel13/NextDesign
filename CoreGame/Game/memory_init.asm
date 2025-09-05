initmemory:
	LD A, (memory)		; Lives
	LD A, (memory + 1)	; Current Level

	LD A, (memory + 2) ; Player X
	LD D, 16
	LD E, A
	MUL D, E
	LD HL, DE

	LD A, (memory + 4) ; Player Y
	LD D, 16
	LD E, A
	MUL D, E

	LD A, (memory + 6)	; Number Levels

	; In 1st level now
	LD HL, (memory + 7)	; X Player Start Position
	LD HL, (memory + 9)	; Y Player Start Position

	LD A, (memory + 11) ; Number Enemies
	LD B, A

	LD (numberenemies), A

	LD HL, enemydata
	LD (enemyplace), HL

	LD IX, memory + 12
	DEC IX
enemies:
	INC IX
	PUSH BC
	LD BC, (enemyplace)

	LD DE, (IX)	; Current Position X
	
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	
	INC IX
	INC IX
	LD DE, (IX)	; Current Position Y
	
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	
	INC IX
	INC IX
	LD A, (IX)	; Sprite
	LD (BC), A
	INC BC
	INC IX
	LD A, (IX)	; Path
	LD (BC), A
	INC BC
	INC IX

	;LD A, (IX)	; Current Step
	LD A, 0
	LD (BC), A
	INC BC
	INC IX

	LD (enemyplace), BC

	LD A, (IX) ; Number Paths
	LD C, A
thepaths:
	INC IX
	LD A, (IX)	; Number Steps

	PUSH BC
	LD BC, (enemyplace)
	LD HL, BC
	LD (HL), A
	INC BC
	LD (enemyplace), BC
	POP BC

	LD B, A

	PUSH BC
steps:
	PUSH BC

	LD BC, (enemyplace)
	
	INC IX
	LD DE, (IX)	; Step Speed (0x0000) 0xFFFF
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	INC IX
	INC IX
	LD DE, (IX)	; Step X
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	INC IX
	INC IX
	LD DE, (IX)	; Step Y
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	INC IX

	LD (enemyplace), BC

	POP BC

	DEC B
	JP NZ, steps

	POP BC

	POP BC
	DEC B
	JP NZ, enemies

	RET