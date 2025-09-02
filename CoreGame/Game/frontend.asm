frontend_init:
; Enable tilemap mode
    NEXTREG $6B, %10100001		; 40x32, 8-bit entries
	NEXTREG $6C, %00000000		; palette offset, visuals

	; Tell harware where to find tiles
	NEXTREG $6E, OFFSET_OF_MAP	; MSB of tilemap in bank 5  40
	NEXTREG $6F, OFFSET_OF_TILES	; MSB of tilemap definitions  32

	; Setup tilemap palette
	NEXTREG $43, %00110000		; Auto increment, select first tilemap palette
	NEXTREG $40, 0			; Start with first entry

	; Copy palette
	LD HL, palette			; Address of palette data in memory
	LD B, 16			; Copy 16 colours
	CALL copyPalette		; Call routine for copying

	; Copy tile definitions to expected memory
	LD HL, tiles			; Address of tiles in memory
	LD BC, tilesLength		; Number of bytes to copy
	CALL copyTileDefinitions	; Copy all tiles data

	; Copy tilemap to expected memory
	LD HL, tilemap			; Addreess of tilemap in memory
	CALL copyTileMap40x32		; Copy 40x32 tilemaps

	; Give it some time
	CALL delay
	CALL delay
	CALL delay
	CALL delay
    
	; Then use offset registers to simulate shake.
	LD A, 1				; Offset by 1 pixel
	LD B, 40			; Number of repetitions
.shakeLoop:
	NEXTREG $30, A			; Set current offset
	LD HL, 5000
	CALL customDelay
	XOR 1				; Change offset: if 0 to 1, then back to 0
	DJNZ .shakeLoop

    ld hl, $0001
    ld (state), hl

	CALL initSprites

    jp start

delay:
	LD HL, $FFFF
customDelay:
	PUSH AF
.loop:
	DEC HL
	LD A, H
	OR L
	JR NZ, .loop

	POP AF
	RET

;;--------------------------------------------------------------------
;; subroutines
;;--------------------------------------------------------------------

;---------------------------------------------------------------------
; HL = memory location of the palette
copyPalette256:
	LD B, 0			; This variant always starts with 0
;---------------------------------------------------------------------
; HL = memory location of the palette
; B = number of colours to copy
copyPalette:
	LD A, (HL)		; Load RRRGGGBB into A
	INC HL			; Increment to next entry
	NEXTREG $41, A		; Send entry to Next HW
	DJNZ copyPalette	; Repeat until B=0
	RET

;---------------------------------------------------------------------
; HL = memory location of tile definitons
; BC = size of tile defitions in bytes.
copyTileDefinitions:
	LD DE, START_OF_TILES
	LDIR
	RET

;---------------------------------------------------------------------
; HL = memory location of tilemap
copyTileMap40x32:
	LD BC, 40*32		; This variant always load 40x32
	JR copyTileMap
copyTileMap80x32:
	LD BC, 80*32		; This variant always loads 80x32
;---------------------------------------------------------------------
; HL = memory location of tilemap
; BC = size of tilemap in bytes
copyTileMap:
	LD DE, START_OF_TILEMAP
	LDIR
	RET

initSprites:
; Load sprite data using DMA
	LD HL, sprites			; Sprites data source
	LD BC, 16*16*5			; Copy 5 sprites, each 16x16 pixels
	LD A, 0				; Start with first sprite
	CALL loadSprites		; Load sprites to FPGA

	; Setup sprite hardware
	NEXTREG $15, %01000001		; sprite 0 on top, SLU, sprites visible

    ; This block from 'Code' project
    NEXTREG $07, 3;           // 28Mhz
    NEXTREG $08,0x4A;        // Disable RAM contention, enable DAC and turbosound
    NEXTREG $05,0x04;        // 60Hz mode
    NEXTREG $15,0x03;       // layer order - and sprites on
    NEXTREG $4B,0xE3;       // sprite transparency

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
enemies:
	PUSH BC
	LD BC, (enemyplace)
; First Enemy (Enemies)
	LD DE, (IX)	; Current Position X
	
	LD HL, BC
	LD D, 16
	MUL D, E
	LD (HL), DE
	INC BC
	INC BC
	
	INC IX
	INC IX
	LD DE, (IX)	; Current Position Y
	
	LD HL, BC
	LD D, 16
	MUL D, E
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

	POP BC
	DEC B
	JP NZ, enemies

; 1st Path (Paths)
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
	LD A, 16
	LD D, A
	MUL D, E
	LD HL, BC
	LD (HL), DE
	INC BC
	INC BC
	INC IX
	INC IX
	LD DE, (IX)	; Step Y
	LD D, 16
	MUL D, E
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

	DEC C
	JP NZ, thepaths

	RET

enemyplace:
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
	LD C, 0

	LD HL, enemydata
	LD IY, HL

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
	LD HL, enemydata
	LD IY, HL

	LD C, (IY + 7)
	LD A, B
	
	CP C

	JP Z, reset
	JP pastreset

reset:
	PUSH IY
	LD HL, enemydata
	LD IY, HL
	LD (IY + 6), 0
	POP IY

pastreset:
	LD HL, enemydata
	LD IY, HL

	INC A
	LD (IY + 6), A

	POP IY

	POP AF
	POP BC

	INC IY
	LD HL, (IY)
	LD A, (IY + 2)
	LD C, A
enemiesloop:

	;LD HL, 300
	;LD C, 30
	CALL showsprite

	;DEC B
	;JP NZ, enemiesloop
    JP start

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