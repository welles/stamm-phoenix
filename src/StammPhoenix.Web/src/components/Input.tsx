import { mergeProps, splitProps } from 'solid-js'

interface InputProps {
	type: string
	id: string
	value: string
	error?: boolean
	required?: boolean
	name: string
	classes: string
}

const Input = (props: InputProps) => {
	const [local, input] = splitProps(
		mergeProps(
			{
				error: false,
				classes: 'border-dpsg-blue',
			},
			props,
		),
		['error', 'classes'],
	)

	return (
		<input
			{...input}
			class={`${local.classes} ${
				local.error ? 'border-dpsg-red' : 'border-dpsg-blue'
			}`}
		/>
	)
}

export default Input
