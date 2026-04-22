#!/usr/bin/env bash

# Иконки (проверь, отображаются ли они у тебя)
shutdown="󰐥"
reboot="󰜉"
lock="󰌾"
suspend="󰤄"
logout="󰍃"
cancel="󰅖"

# Запускаем Rofi и получаем выбор
chosen=$(echo -e "$shutdown\n$reboot\n$lock\n$suspend\n$logout\n$cancel" | rofi -dmenu -config ~/.config/rofi/powermenu.rasi)

# Выполняем команду
case $chosen in
    $shutdown)
        shutdown now
        ;;
    $reboot)
        reboot
        ;;
    $lock)
        swaylock # Измени, если используешь другой локер (например, hyprlock)
        ;;
    $suspend)
        systemctl suspend
        ;;
    $logout)
        hyprctl dispatch exit
        ;;
    *)
        exit 1
        ;;
esac