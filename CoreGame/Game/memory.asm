memory:
    INCBIN "memory.map"

local:
    db 0x00         ; Lives
    db 0x00         ; Current Level
    db 0x00, 0x00    ; X
    db 0x00, 0x00    ; Y
