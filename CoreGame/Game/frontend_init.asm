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
