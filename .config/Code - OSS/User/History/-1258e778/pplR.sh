#!/usr/bin/env bash

# Иконки для главного меню
shutdown="󰐥"
reboot="󰜉"
lock="󰌾"
suspend="󰤄"
logout="󰍃"
cancel="󰅖"

# Иконки для подтверждения
yes=""
no="󰅖"

# 1. Главное меню
chosen=$(echo -e "$shutdown\n$reboot\n$lock\n$suspend\n$logout\n$cancel" | rofi -dmenu -config ~/.config/rofi/powermenu.rasi -p "Power Menu")

# 2. Функция подтверждения
confirm_exit() {
    echo -e "$yes\n$no" | rofi -dmenu -config ~/.config/rofi/powermenu.rasi -p "Are you sure?"
}

# 3. Логика выполнения
case $chosen in
    $shutdown)
        if [ "$(confirm_exit)" == "$yes" ]; then
            shutdown now
        fi
        ;;
    $reboot)
        if [ "$(confirm_exit)" == "$yes" ]; then
            reboot
        fi
        ;;
    $lock)
        hyprlock # Или swaylock, смотря что у тебя стоит
        ;;
    $suspend)
        systemctl suspend
        ;;
    $logout)
        if [ "$(confirm_exit)" == "$yes" ]; then
            hyprctl dispatch exit
        fi
        ;;
    *)
        exit 1
        ;;
esac