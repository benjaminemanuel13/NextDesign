
;---------------------------------------------------------------------
;; sjasmplus setup
;;--------------------------------------------------------------------
	
	; Allow Next paging and instructions
	DEVICE ZXSPECTRUMNEXT
	SLDOPT COMMENT WPMEM, LOGPOINT, ASSERTION
	
	; Generate a map file for use with Cspect
	CSPECTMAP "build/test.map"


;;--------------------------------------------------------------------
;; program
;;--------------------------------------------------------------------

	ORG $8000 

START_OF_BANK_5		EQU $4000
START_OF_TILEMAP	EQU $6000	; Just after ULA attributes
START_OF_TILES		EQU $6600	; Just after 40x32 tilemap

OFFSET_OF_MAP		EQU (START_OF_TILEMAP - START_OF_BANK_5) >> 8
OFFSET_OF_TILES		EQU (START_OF_TILES - START_OF_BANK_5) >> 8

go:
    call SetUpIRQs
start:  
    ld hl, (state)
    
    ld de, $0000
    OR A
    sbc hl, de
    jp z, frontend_init

    ld de, $0001
    OR A
    sbc hl, de
    jp z, frontend_main

    ld de, $0002
    OR A
    sbc hl, de
    jp z, game_init

    ld de, $0003
    OR A
    sbc hl, de
    jp z, game_main

    jp start

;;--------------------------------------------------------------------
;; Data Section
;;--------------------------------------------------------------------

state:
    db 0x00, 0x00

;;--------------------------------------------------------------------
;; Includes
;;--------------------------------------------------------------------

    INCLUDE "frontend.asm"
    INCLUDE "game.asm"
    INCLUDE "memory.asm"
    INCLUDE "gamedata.asm"
    INCLUDE "irq.asm"

;;--------------------------------------------------------------------
;; Set up .nex output
;;--------------------------------------------------------------------

	; This sets the name of the project, the start address, 
	; and the initial stack pointer.
	SAVENEX OPEN "build/test.nex", start, $ff40

	; This asserts the minimum core version.  Set it to the core version 
	; you are developing on.
	SAVENEX CORE 2,0,0

	; This sets the border colour while loading (in this case white),
	; what to do with the file handle of the nex file when starting (0 = 
	; close file handle as we're not going to access the project.nex 
	; file after starting.  See sjasmplus documentation), whether
	; we preserve the next registers (0 = no, we set to default), and 
	; whether we require the full 2MB expansion (0 = no we don't).
	SAVENEX CFG 7,0,0,0

	; Generate the Nex file automatically based on which pages you use.
	SAVENEX AUTO

