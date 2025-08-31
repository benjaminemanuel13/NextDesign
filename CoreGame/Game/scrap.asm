; Show combined sprite 1-4 using patterns 1-4
	NEXTREG $34, 2			; Select third sprite
	NEXTREG $35, 150		; X=150
	NEXTREG $36, 80			; Y=80
	NEXTREG $37, %00000000		; Palette offset, no mirror, no rotation
	NEXTREG $38, %11000001		; Visible, use byte 4, pattern 1
	NEXTREG $79, %00100000		; Anchor with unified relatives, no scaling

	NEXTREG $35, 16			; X=AnchorX+16
	NEXTREG $36, 0			; Y=AnchorY+0
	NEXTREG $37, %00000000		; Palette offset, no mirror, no rotation
	NEXTREG $38, %11000010		; Visible, use byte 4, pattern 2
	NEXTREG $79, %01000000		; Relative sprite

	NEXTREG $35, 0			; X=AnchorX+0
	NEXTREG $36, 16			; Y=AnchorY+16
	NEXTREG $37, %00000000		; Palette offset, no mirror, no rotation
	NEXTREG $38, %11000011		; Visible, use byte 4, pattern 3
	NEXTREG $79, %01000000		; Relative sprite

	NEXTREG $35, 16			; X=AnchorX+16
	NEXTREG $36, 16			; Y=AnchorY+16
	NEXTREG $37, %00000000		; Palette offset, no mirror, no rotation
	NEXTREG $38, %11000100		; Visible, use byte 4, pattern 4
	NEXTREG $79, %01000000		; Relative sprite

    ; Update our relative sprite again:
	; - change position
	; - mirror X
	; - scale X&Y 2x
	NEXTREG $34, 2			; Select third sprite
	NEXTREG $35, 220		; X=220
	NEXTREG $36, 120		; Y=120
	NEXTREG $37, %00001010		; Palette offset, mirror X, rotate
	NEXTREG $38, %11000001		; Visible, use byte 4, pattern 1
	NEXTREG $39, %00101010		; Anchor with unified relatives, scale X&Y 
    ;---------------------------------------------------------------------

    ; Wait for a while
	call delay

	; Update our relative sprite:
	; - change position
	; - rotate
	; - scale X 2x
	;NEXTREG $34, 2			; Select third sprite
	;NEXTREG $35, 200		; X=200
	;NEXTREG $36, 100		; Y=100
	;NEXTREG $37, %00000010		; Palette offset, no mirror, rotate
	;NEXTREG $38, %11000001		; Visible, use byte 4, pattern 1
	;NEXTREG $39, %00101000		; Anchor with unified relatives, scale X 

	call delay


	//LD A, (memory + 1) 	; Level
	LD A, (memory + 2) ; Player X
	LD D, 16
	LD E, A
	MUL D, E
	LD HL, DE

	LD A, (memory + 4) ; Player Y
	LD D, 16
	LD E, A
	MUL D, E