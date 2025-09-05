	include "frontend_init.asm"
	include "memory_init.asm"

enemyplace:
	db 0x00

nosteps:
	db 0x00

; Tilemap settings: 8px, 40x32, disable "include header" when downloading, file is then usabe as is.
tilemap:
	INCBIN "tileMap.map"
tilemapLength: EQU $ - tilemap

; Sprite Editor settings: 4bit, after downloading manually removed empty data (with HxD) to only leave first 192 bytes.
tiles:
	include "tiles.asm"

tilesLength: EQU $ - tiles

; After setting up palette, used Download button and then manually removed every second byte (with HxD) and only left 16 entries (so 16 bytes)
palette:
	;INCBIN "tiles.pal"
    
	//db 0x00, 0x1C, 0xF0, 0xAC, 0x20, 0x24, 0x1C, 0x34, 0x08, 0x09, 0x0A, 0x0B, 0x0C, 0x0D, 0x0E, 0x0F
	
	db 0x8C // Olive
	db 0x0D // Dark Green
    db 0x89 // Brown
	db 0x38 // Teal
	db 0xE0 // Red
	;db 0x03 // Blue
	db 0x8C // Olive
    db 0xFC // Yellow
    db 0xE3 // Magenta (Transparent)
    db 0x1F // Cyan
    db 0xFF // White
    db 0x70 // Dark Red
    db 0x07 // Dark Blue
    db 0x00 // Black
    db 0xC7 // Purple
    db 0x92 // Gray
    db 0xAA // Light Gray


paletteLength: EQU $-palette

// Initially A is Sprite, HL is X Pos, C is Y Pos
showsprite:
	
	; Show single sprite 0 using pattern 0
	NEXTREG $34, A				; First sprite
	LD A, L
	NEXTREG $35, A				; X=100
	LD A, C
	NEXTREG $36, A				; Y=80
	LD A, 0
	ADD A, H
	NEXTREG $37, A				; Palette offset, no mirror, no rotation
	NEXTREG $38, %10000000		; Visible, no byte 4, pattern 0
	RET

spritepos:
	db 0x00

frontend_main:
	; Show single sprite 1 using pattern 0
	;NEXTREG $34, 0			; Second sprite
	;NEXTREG $35, 84			; X=84
	;NEXTREG $36, 80			; Y=80
	;NEXTREG $37, %00000000		; Palette offset, no mirror, no rotation
	;NEXTREG $38, %10000000		; Visible, no byte 4, pattern 0
enemiesgo:
	LD A, (numberenemies)
	LD B, A

	LD (enemynum), A

	LD HL, enemydata
	LD (enemyhold), HL
enemyport:
	LD HL, (enemyhold)
	LD A, (HL)
	ADD A, 4
	LD (spritepos), A

	LD IY, HL
	;LD C, 0

	LD A, (IY + 5)	; sprite
	
	PUSH AF
	PUSH BC

	; Find Current Step
	LD A, (IY + 6)

	LD B, A 	; current step
	LD C, 0xFF		; counter

	ADD HL, 3
	LD IY, HL

enemymove:
	INC C
	ADD HL, 6

	LD IY, HL
	LD A, C
	CP B
	JP C, enemymove ; (Counter) < Less

	PUSH IY
	LD HL, (enemyhold)
	LD IY, HL

	LD C, (IY + 7)
	LD A, C
	LD (numsteps), A
	LD A, B
	
	INC A

	CP C

	JP Z, reset
	JP pastreset

reset:
	LD A, 0x00
pastreset:
	DEC A
	LD HL, (enemyhold)
	LD IY, HL

	INC A
	LD (IY + 6), A

	POP IY
	POP BC
	POP AF

	PUSH IY

	INC IY
	LD HL, (IY)
	LD A, (IY + 2)
	LD C, A
enemiesloop:
	PUSH AF
	LD A, (spritepos)
	CALL showsprite
	POP AF

	PUSH HL
	CALL delay
	POP HL

	POP IY

	LD HL, IY

	LD HL, (enemyhold)
	LD A, (numsteps)
	LD D, 6
	LD E, A
	MUL D, E
	ADD HL, DE
	ADD HL, 8

	LD (enemyhold), HL

	DEC B
	JP NZ, enemyport

    JP start

enemyhold:
	db 0x00, 0x00

enemynum:
	db 0x00

numsteps:
	db 0x00

; HL = address of sprite sheet in memory
; BC = number of bytes to load
; A  = index of first sprite to load int5o
loadSprites:
	LD (.dmaSource), HL	; Copy sprite sheet address from HL
	LD (.dmaLength), BC	; Copy length in bytes from BC
	LD BC, $303B		; Prepare port for sprite index
	OUT (C), A		; Load index of first sprite
	LD HL, .dmaProgram	; Setup source for OTIR
	LD B, .dmaProgramLength	; Setup length for OTIR
	LD C, $6B		; Setup DMA port
	OTIR			; Invoke DMA code
	RET
.dmaProgram:
	DB %10000011		; WR6 - Disable DMA
	DB %01111101		; WR0 - append length + port A address, A->B
.dmaSource:
	DW 0			; WR0 par 1&2 - port A start address
.dmaLength:
	DW 0			; WR0 par 3&4 - transfer length
	DB %00010100		; WR1 - A incr., A=memory
	DB %00101000		; WR2 - B fixed, B=I/O
	DB %10101101		; WR4 - continuous, append port B address
	DW $005B		; WR4 par 1&2 - port B address
	DB %10000010		; WR5 - stop on end of block, CE only
	DB %11001111		; WR6 - load addresses into DMA counters
	DB %10000111		; WR6 - enable DMA
.dmaProgramLength = $-.dmaProgram

;;--------------------------------------------------------------------
;; data
;;--------------------------------------------------------------------

	include "sprites.asm"