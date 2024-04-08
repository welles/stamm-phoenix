import type { ParentProps } from "solid-js"

interface ButtonProps extends ParentProps {
	type: "button" | "submit" | "reset"
	classes: string
}

const Button = (props: ButtonProps) => {
	return (
		<button {...props} class={props.classes}>
			{props.children}
		</button>
	)
}

export default Button
